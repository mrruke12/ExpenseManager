using ExpenseManager.Core.Entities;

namespace ExpenseManager.Core.Interfaces.Repositories {
    public interface IBankTransferRepository {
        Task<List<BankTransfer>> GetTransfersForUser(AppUser? user, BankTransferFilter filter);
        Task<BankTransfer?> GetByIdForUser(AppUser? user, int id);
        Task<bool> UpdateForUserAsync(AppUser? user, BankTransfer transfer);
        Task<bool> DeleteForUser(AppUser? user, int id);
        Task<BankTransfer> AddAsync(BankTransfer transfer);
        Task<List<BankTransfer>> GetTransfersForPeriodForUserAsync(AppUser? user, DateOnly From, DateOnly To);
    }
}
