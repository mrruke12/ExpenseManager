using ExpenseManager.Core.Entities;

namespace ExpenseManager.Core.Interfaces.Builders {
    public interface IAppUserBuilder {
        IAppUserBuilder Reset(AppUser? prototype = null);
        IAppUserBuilder Reference(AppUser prototype);
        IAppUserBuilder SetEmail(string email);
        IAppUserBuilder SetPassword(string password);
        IAppUserBuilder SetLanguageCode(string code);
        IAppUserBuilder SetTelegramId(long id);
        AppUser Build();
    }
}
