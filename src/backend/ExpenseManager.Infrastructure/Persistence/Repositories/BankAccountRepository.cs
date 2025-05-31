using ExpenseManager.Core.Entities;
using ExpenseManager.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Persistence.Repositories {
    public class BankAccountRepository : IBankAccountRepository {
        private readonly AppDbContext _context;

        public BankAccountRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<BankAccount> AddAsync(BankAccount account) {
            var existing = await _context.Accounts.FirstOrDefaultAsync(a => a.Name == account.Name);

            if (existing != null) return existing;


            await _context.AddAsync(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<bool> DeleteForUserAsync(AppUser? user, int id) {
            if (user == null) return false;

            await _context.Accounts.Where(a => a.Id == id && a.UserId == user.Id).ExecuteDeleteAsync();
            return true;
        }

        public async Task<List<BankTransfer>> GetAccountTransfersAsync(int id, AppUser? user, BankTransferFilter filter) {
            if (user == null) return new List<BankTransfer>();

            var allTransfers = (await _context.Accounts.Include(a => a.Transfers).FirstOrDefaultAsync(a => a.Id == id && a.UserId == user.Id)).Transfers;
            var filtered = FilterTransfers(allTransfers, filter);

            return filtered.ToList();
        }

        private IEnumerable<BankTransfer> FilterTransfers(IEnumerable<BankTransfer> allTransfers, BankTransferFilter filter) {
            var filtered = allTransfers.Where(t =>
                                                       (t.Moment >= filter.FromDate && t.Moment <= filter.ToDate) &&
                                                       (t.Bill >= filter.MinBill && t.Bill <= filter.MaxBill));

            if (filter.CategoryId != -1) filtered = filtered.Where(t => t.Category.Id == filter.CategoryId);

            return filtered;
        }

        public async Task<BankAccount?> GetByIdForUserAsync(AppUser? user, int id) {
            if (user == null) return null;

            return await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id && a.UserId == user.Id);
        }

        public async Task<BankAccount?> GetByNameForUserAsync(AppUser? user, string name) {
            if (user == null) return null;

            return await _context.Accounts.FirstOrDefaultAsync(a => a.Name.ToLower().Trim() == name.ToLower().Trim() && a.UserId == user.Id);
        }

        public async Task<List<BankAccount>> GetByUserAsync(AppUser? user) {
            if (user == null) return new List<BankAccount>();

            return await _context.Accounts.Where(a => a.UserId == user.Id).ToListAsync();
        }

        public async Task<bool> UpdateForUserAsync(AppUser? user, BankAccount account) {
            if (user == null) return false;

            _context.Update(account);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
