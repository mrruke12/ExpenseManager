using ExpenseManager.Core.Entities;
using ExpenseManager.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Persistence.Repositories {
    public class BankTransferRepository : IBankTransferRepository {
        private readonly AppDbContext _context;

        public BankTransferRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<BankTransfer> AddAsync(BankTransfer transfer) {
            await _context.Transfers.AddAsync(transfer);
            await _context.SaveChangesAsync();
            return transfer;
        }

        public async Task<bool> DeleteForUser(AppUser? user, int id) {
            if (user == null) return false;

            await _context.Transfers.Include(t => t.Account).Where(t => t.Id == id && t.Account.UserId == user.Id).ExecuteDeleteAsync();
            return true;
        }

        public async Task<BankTransfer?> GetByIdForUser(AppUser? user, int id) {
            if (user == null) return null;

            return await _context.Transfers.Include(t => t.Account).FirstOrDefaultAsync(t => t.Id == id && t.Account.UserId == user.Id);
        }

        public async Task<List<BankTransfer>?> GetTransfersForUser(AppUser? user, BankTransferFilter filter) {
            if (user == null) return null;

            var allTransfers = _context.Transfers.Include(t => t.Account).Where(t => t.Account.UserId == user.Id);
            var filtered = FilterTransfers(allTransfers, filter);

            return filtered.ToList();
        }

        private IEnumerable<BankTransfer> FilterTransfers(IEnumerable<BankTransfer> allTransfers, BankTransferFilter filter) {
            var filtered = allTransfers.Where(t =>
                                                       (t.Moment.Ticks >= filter.FromDate.Ticks && t.Moment.Ticks <= filter.ToDate.Ticks) &&
                                                       (t.Bill >= filter.MinBill && t.Bill <= filter.MaxBill));

            if (filter.CategoryId != null) filtered = filtered.Where(t => t.Category.Id == filter.CategoryId);

            return filtered;
        }

        public async Task<bool> UpdateForUserAsync(AppUser? user, BankTransfer transfer) {
            if (user == null) return false;

            _context.Update(transfer);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
