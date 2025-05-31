using ExpenseManager.Core.Entities;

namespace ExpenseManager.Core.Interfaces.Repositories {
    public interface IBankTransferCategoryRepository {
        Task<List<BankTransferCategory>> GetForUserAsync(AppUser? user);
        Task<BankTransferCategory?> GetByIdForUserAsync(AppUser? user, int id);
        Task<BankTransferCategory?> GetByNameForUserAsync(AppUser? user, string name);
        Task<BankTransferCategory> AddAsync(BankTransferCategory category);
        Task<bool> UpdateForUserAsync(AppUser? user, BankTransferCategory category);
        Task<bool> DeleteForUserAsync(AppUser? user, int id);
    }
}
