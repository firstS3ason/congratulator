using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using File = System.IO.File;
using Telegram.Bot.Polling;
using System.Text.RegularExpressions;
using System.Net.Http.Json;
using WebApplication5LVL.AppData.Contexts.ExtensionsMethods;

namespace WebApplication5LVL.AppData.Contexts.Telegram
{
    public sealed class BotService : BotServiceBase
    {
        public BotService() : base(new TelegramBotClient("6079642455:AAHclpVqyNytPxupauG1dKaqJaMQvzyajOY"))
        {
            client.SetMyCommandsAsync(new List<BotCommand>()
            {
                new BotCommand() { Command = "/start"},
                new BotCommand()
                { 
                    Command = "/birthDay",
                    Description = "To get info about when birthday of selected person"
                }
            });
        }
        public override async Task StartBotAsync(ReceiverOptions receiveOptions, CancellationToken token)
        {
            await Task.Run( () => client
            .StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiveOptions, token));
        }
        public override async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        =>  await File.AppendAllTextAsync(pathToLogFile,Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        
        public override async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            try
            {
                Regex commandRegex = new Regex(@"^/");
                Regex guidRegex = new Regex("^(?:\\{{0,1}(?:[0-9a-fA-F]){8}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){12}\\}{0,1})$");

                if (update.Type == UpdateType.Message)
                {
                    Message? msg = update.Message;
                    string? usersText = msg?.Text;

                    if (commandRegex.IsMatch(usersText.ToLower()))
                    {
                        switch (usersText)
                        {
                            case "/start":
                                await client.SendTextMessageAsync(msg.Chat, "Welcome to our, WHITE-mood congratulations sending service, folk!\r\n\n");
                                break;
                            case "/birthday":
                                await client.SendTextMessageAsync(msg.Chat, "Send user's identifier");
                                break;
                            default:
                                await client.SendTextMessageAsync(msg.Chat, "Unknown command"); 
                                break;
                        }
                    }
                    else if (guidRegex.IsMatch(usersText))
                    {
                     
                        using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri($"https://localhost:7119/getById?id={usersText}"));
                        using HttpClient httpClient = new HttpClient(new HttpClientHandler());

                        using HttpResponseMessage response = await httpClient.SendAsync(request);
                        Domain.Models.User? foundUser = await response.Content.ReadFromJsonAsync<Domain.Models.User>();
                        await client.SendTextMessageAsync(msg.Chat, (foundUser.birthDay.GetNextBirthdayDate() - DateTime.Now).Days.ToString());
                    }
                    else
                        await client.SendTextMessageAsync(msg.Chat, "Understandable");
                }
            }
            catch(Exception ex)
            {
                await File.AppendAllTextAsync(pathToLogFile, ex.Message);
            }
        }
    }
}
