using ExpenseManager.Core.Interfaces.Repositories;
using ExpenseManager.Core.Services;
using Telegram.Bot;
using Telegram.Bot.Types;


namespace ExpenseManager.Bot.Commands {
    public class DefaultCommand : IBotCommand {
        private readonly LocalizationService _localization;
        private readonly IAppUserRepository _userRepository;

        public DefaultCommand(IAppUserRepository userRepository, LocalizationService localization) {
            _localization = localization;
            _userRepository = userRepository;
        }

        public async Task ExecuteAsync(TelegramBotClient bot, long chatId, long TelegramId, Update update) {
            var culture = await _userRepository.GetCultureAsync(TelegramId);

            await bot.SendMessage(chatId, _localization[culture].DefaultMessage);
        }
    }
}
