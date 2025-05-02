using ExpenseManager.Core.Entities;

namespace ExpenseManager.Core.Interfaces {
    public interface IAppUserRepository {
        Task<AppUser> AddAsync(AppUser user);
        Task<AppUser?> GetByTelegramIdAsync(long id);
        Task<AppUser?> GetByIdAsync(string id);
        Task<AppUser?> GetByEmailAsync(string email);
        Task DeleteAsync(string id);
        Task UpdateAsync(AppUser user);
        Task<AppUser?> ValidateAsync(string email, string password);
        Task<string> GetCultureAsync(long id);
    }
}
