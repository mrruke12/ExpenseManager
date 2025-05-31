using ExpenseManager.Core.Entities;
using ExpenseManager.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Persistence.Repositories {
    public class BankTransferCategoryRepository : IBankTransferCategoryRepository {
        private readonly AppDbContext _context;

        public BankTransferCategoryRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<BankTransferCategory> AddAsync(BankTransferCategory category) {
            await _context.TransfersCategories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteForUserAsync(AppUser? user, int id) {
            if (user == null) return false;

            await _context.TransfersCategories.Where(c => c.Id == id && c.UserId == user.Id).ExecuteDeleteAsync();
            return true;
        }

        public async Task<BankTransferCategory?> GetByIdForUserAsync(AppUser? user, int id) {
            if (user == null) return null;

            return await _context.TransfersCategories.FirstOrDefaultAsync(c => c.Id == id && c.UserId == user.Id);
        }

        public async Task<BankTransferCategory?> GetByNameForUserAsync(AppUser? user, string name) {
            if (user == null) return null;

            return await _context.TransfersCategories.FirstOrDefaultAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.UserId == user.Id);
        }

        public async Task<List<BankTransferCategory>> GetForUserAsync(AppUser? user) {
            if (user == null) return new List<BankTransferCategory>();

            return await _context.TransfersCategories.Where(c => c.UserId == user.Id).ToListAsync();
        }

        public async Task<bool> UpdateForUserAsync(AppUser? user, BankTransferCategory category) {
            if (user == null) return false;

            _context.TransfersCategories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
