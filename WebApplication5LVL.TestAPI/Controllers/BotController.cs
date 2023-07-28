using Microsoft.AspNetCore.Mvc;
using System.Net;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using WebApplication5LVL.AppData.Contexts.Telegram;

namespace WebApplication5LVL.TestAPI.Controllers
{
    /// <summary>
    /// BotController service. Holding endPoints with midllewareComponents to handle HTTP-requests
    /// </summary>
    [ApiController()]
    public class BotController : ControllerBase
    {
        private readonly BotServiceBase botService;
        public BotController(BotServiceBase _botService)
            => (botService) = (_botService);
        /// <summary>
        /// Request to start telegram bot's activity async
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("/startBot")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> StartBotAsync(CancellationToken token = default)
        {
            await botService.StartBotAsync(new ReceiverOptions() { AllowedUpdates = new UpdateType[] { UpdateType.Message } });
            return Ok();
        }
    }
}
