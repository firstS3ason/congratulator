
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using WebApplication5LVL.AppData.Contexts.User;

namespace WebApplication5LVL.AppData.Contexts.Telegram
{
    public abstract class BotServiceBase
    {
        protected readonly IUserService userService;
        protected readonly string pathToLogFile;
        private ITelegramBotClient _client;
        protected ITelegramBotClient client
        {
            get => _client;
            set
            {
                if (!Equals(_client, value))
                    _client = value;
            }
        }
        public BotServiceBase(ITelegramBotClient _botClient, IUserService _userService)
        {
            client = _botClient;
            pathToLogFile = $"{Directory.GetCurrentDirectory()}/{client.GetMeAsync().Result.FirstName}.txt";

            userService = _userService;
        }
        public abstract Task HandleUpdateAsync(ITelegramBotClient botClient,Update update, CancellationToken token);
        public abstract Task StartBotAsync(ReceiverOptions receiveOptions, CancellationToken token = default);
        public abstract Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken);
    }
}
