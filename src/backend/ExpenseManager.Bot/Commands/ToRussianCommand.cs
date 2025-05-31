using ExpenseManager.Core.Interfaces.Repositories;
using ExpenseManager.Core.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ExpenseManager.Bot.Commands {
    public class ToRussianCommand : IBotCommand {
        private readonly IAppUserRepository _userRepository;
        private readonly LocalizationService _localizationService;

        public ToRussianCommand(IAppUserRepository userRepository, LocalizationService localizationService) {
            _userRepository = userRepository;
            _localizationService = localizationService;
        }

        public async Task ExecuteAsync(TelegramBotClient bot, long chatId, long TelegramId, Update update) {
            var user = await _userRepository.GetByTelegramIdAsync(TelegramId);
            var culture = user.LanguageCode = "ru";
            await _userRepository.UpdateAsync(user);

            await bot.SendMessage(
                chatId: chatId,
                text: _localizationService[culture].ToRussianMessage
            );
        }
    }
}
