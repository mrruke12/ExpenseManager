using Telegram.Bot;
using Telegram.Bot.Types;

namespace ExpenseManager.Bot.Commands {
    public interface IBotCommand {
        Task ExecuteAsync(TelegramBotClient bot, long chatId, long userId, Update update);
    }
}
