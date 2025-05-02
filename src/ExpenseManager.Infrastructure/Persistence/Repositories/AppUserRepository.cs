using ExpenseManager.Core.Entities;
using ExpenseManager.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ExpenseManager.Infrastructure.Persistence.Repositories {
    public class AppUserRepository : IAppUserRepository {
        private readonly AppDbContext _context;

        public AppUserRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<AppUser> AddAsync(AppUser user) {
            var existing = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existing != null) {
                Log.Warning($"User with id \"{user.Id}\" and email \"{user.Email}\" already exists");
                return existing;
            }

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
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<AppUser?> GetByIdAsync(string id) {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateAsync(AppUser user) {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<AppUser?> ValidateAsync(string email, string password) {
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
    }
}
