using ExpenseManager.Core.Entities;
using ExpenseManager.Core.Interfaces.Repositories;
using ExpenseManager.Core.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ExpenseManager.Bot.Commands {
    public class StartCommand : IBotCommand {
        private readonly IAppUserRepository _userRepository;
        private readonly LocalizationService _localizationService;

        public StartCommand(IAppUserRepository userRepository, LocalizationService localizationService) {
            _userRepository = userRepository;
            _localizationService = localizationService;
        }

        public async Task ExecuteAsync(TelegramBotClient bot, long chatId, long TelegramId, Update update) {
            string id = ExtractIdFromMessage(update.Message.Text);

            AppUser? user = await _userRepository.GetByIdAsync(id);
            if (user != null && user.TelegramId != TelegramId) {
                user.TelegramId = TelegramId;
                await _userRepository.UpdateAsync(user);
            }

            if (user == null) user = await _userRepository.GetByTelegramIdAsync(TelegramId);

            var culture = user?.LanguageCode;

            string responseMessage = user == null
                ? _localizationService[culture].NewUserStartMessage
                : _localizationService[culture].StartMessage;

            await bot.SendMessage(
                chatId: chatId,
                text: responseMessage
            );
        }

        private static string ExtractIdFromMessage(string messageText) {
            string lastWord = messageText.Split(' ').LastOrDefault() ?? string.Empty;
            return new string(lastWord.Reverse().ToArray());
        }
    }
}
