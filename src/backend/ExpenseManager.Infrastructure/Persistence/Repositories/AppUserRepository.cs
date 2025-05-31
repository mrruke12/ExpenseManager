using ExpenseManager.Core.Entities;
using ExpenseManager.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExpenseManager.Infrastructure.Persistence.Repositories {
    public class AppUserRepository : IAppUserRepository {
        private readonly AppDbContext _context;

        public AppUserRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<AppUser> AddAsync(AppUser user) {
            var existing = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existing != null) return existing;

            while (true) {
                existing = await GetByIdAsync(user.Id);
                if (existing != null) user.Id = Guid.NewGuid().ToString();
                else break;
            }

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task DeleteAsync(string id) {
            await _context.Users.Where(u => u.Id == id).ExecuteDeleteAsync();
        }

        public async Task<AppUser?> GetByTelegramIdAsync(long id) {
            return await _context.Users.FirstOrDefaultAsync(u => u.TelegramId == id);
        }

        public async Task<AppUser?> GetByIdAsync(string id) {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateAsync(AppUser user) {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<AppUser?> ValidateLoginAsync(string email, string password) {
            var users = _context.Users;
            var user = await GetByEmailAsync(email);

            if (user == null) return null;

            if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return user;

            return null;
        }

        public async Task<AppUser?> GetByEmailAsync(string email) {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<string> GetCultureAsync(long id) {
            var user = await GetByTelegramIdAsync(id);
            return user.LanguageCode;
        }

        public async Task<AppUser?> GetByClaimsAsync(ClaimsPrincipal claims) {
            var email = claims.FindFirst("Email")?.Value ?? "";
            var user = await GetByEmailAsync(email);
            return user;
        }

        public async Task<AppUser?> GetByClaimsWithAttachmentsAsync(ClaimsPrincipal claims) {
            var email = claims.FindFirst("Email")?.Value ?? "";

            return await _context.Users.Include(u => u.Categories)
                                       .Include(u => u.Accounts)
                                       .ThenInclude((BankAccount a) => a.Transfers)
                                       .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
