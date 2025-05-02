using ExpenseManager.Bot.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ExpenseManager.Bot {
    public class BotManager : BackgroundService {

        private TelegramBotClient _bot;
        private CancellationTokenSource _cts;
        private readonly IServiceProvider _provider;


        public BotManager(string token, IServiceProvider provider) {
            _cts = new CancellationTokenSource();
            _bot = new TelegramBotClient(token, cancellationToken: _cts.Token);
            _provider = provider;
        }

        async Task HandleUpdateAsync(Update update) {
            // Обработка сообщений
            if (update.Message is { } msg) {
                using (var scope = _provider.CreateScope()) {
                    var factory = new CommandFactory(scope.ServiceProvider);

                    string key = (msg?.Text ?? "").Trim().Split(" ")[0];
                    IBotCommand command = factory.CreateCommand(key, msg.From.Id);

                    await command.ExecuteAsync(_bot, msg.Chat.Id, msg.From.Id, update);
                }
                return;
            }

            // Обработка callback-запросов (кнопки)
            if (update.CallbackQuery is { } callbackQuery) {

                return;
            }

            // Обработка inline-запросов
            if (update.InlineQuery is { } inlineQuery) {

                return;
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) {
            _bot.OnUpdate += HandleUpdateAsync;
            return Task.CompletedTask;
        }
    }
}
