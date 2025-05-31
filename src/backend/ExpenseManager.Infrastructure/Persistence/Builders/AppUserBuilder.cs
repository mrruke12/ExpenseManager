using ExpenseManager.Core.Entities;
using ExpenseManager.Core.Interfaces.Builders;

namespace ExpenseManager.Infrastructure.Persistence.Builders {
    public class AppUserBuilder : IAppUserBuilder {
        private AppUser User;

        public AppUser Build() {
            return User;
        }

        public IAppUserBuilder Reference(AppUser prototype) {
            User = prototype;
            return this;
        }

        public IAppUserBuilder Reset(AppUser? prototype = null) {
            if (prototype == null) User = new AppUser();
            else {
                User = new AppUser {
                    Id = prototype.Id,
                    Email = prototype.Email,
                    TelegramId = prototype.TelegramId,
                    PasswordHash = prototype.PasswordHash,
                    LanguageCode = prototype.LanguageCode
                };
            }
            return this;
        }

        public IAppUserBuilder SetEmail(string email) {
            User.Email = email;
            return this;
        }

        public IAppUserBuilder SetLanguageCode(string code) {
            User.LanguageCode = code;
            return this;
        }

        public IAppUserBuilder SetPassword(string password) {
            User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            return this;
        }

        public IAppUserBuilder SetTelegramId(long id) {
            User.TelegramId = id;
            return this;
        }
    }
}
