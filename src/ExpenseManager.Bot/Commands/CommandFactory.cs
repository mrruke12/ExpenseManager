using ExpenseManager.Core.Interfaces;
using ExpenseManager.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseManager.Bot.Commands {
    public class CommandFactory {
        private readonly IServiceProvider _serviceProvider;

        public CommandFactory(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public IBotCommand CreateCommand(string key, long userId) {
            IAppUserRepository userRepository = _serviceProvider.GetRequiredService<IAppUserRepository>();
            LocalizationService localizationService = _serviceProvider.GetRequiredService<LocalizationService>();

            return key switch {
                "/start" => new StartCommand(userRepository, localizationService),
                "/russian" => new ToRussianCommand(userRepository, localizationService),
                "/english" => new ToEnglishCommand(userRepository, localizationService),
                _ => new DefaultCommand(userRepository, localizationService)
            };
        }
    }
}
