using ExpenseManager.Core.Entities;
using System.Security.Claims;

namespace ExpenseManager.Core.Interfaces.Repositories {
    public interface IAppUserRepository {
        Task<AppUser> AddAsync(AppUser user);
        Task<AppUser?> GetByTelegramIdAsync(long id);
        Task<AppUser?> GetByIdAsync(string id);
        Task<AppUser?> GetByEmailAsync(string email);
        Task DeleteAsync(string id);
        Task UpdateAsync(AppUser user);
        Task<AppUser?> ValidateLoginAsync(string email, string password);
        Task<AppUser?> GetByClaimsAsync(ClaimsPrincipal claims);
        Task<string> GetCultureAsync(long id);
        Task<AppUser?> GetByEmailWithAttachmentsAsync(string email);
    }
}
