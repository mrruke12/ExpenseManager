using ExpenseManager.Core.Interfaces;
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

        public async Task ExecuteAsync(TelegramBotClient bot, long chatId, long userId, Update update) {
            var culture = await _userRepository.GetCultureAsync(userId);

            await bot.SendMessage(chatId, _localization[culture].DefaultMessage);
        }
    }
}
