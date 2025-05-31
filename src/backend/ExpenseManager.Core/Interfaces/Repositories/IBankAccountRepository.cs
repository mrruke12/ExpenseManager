using ExpenseManager.Core.Entities;

namespace ExpenseManager.Core.Interfaces.Repositories {
    public interface IBankAccountRepository {
        Task<BankAccount?> GetByIdForUserAsync(AppUser? user, int id);
        Task<List<BankAccount>> GetByUserAsync(AppUser? user);
        Task<BankAccount?> GetByNameForUserAsync(AppUser? user, string name);
        Task<bool> UpdateForUserAsync(AppUser? user, BankAccount account);
        Task<bool> DeleteForUserAsync(AppUser? user, int id);
        Task<BankAccount> AddAsync(BankAccount account);
        Task<List<BankTransfer>> GetAccountTransfersAsync(int id, AppUser? user, BankTransferFilter filter);
    }
}
