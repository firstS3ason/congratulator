
namespace WebApplication5LVL.AppData.Contexts.Mail
{
    public interface IMailService
    {
        /// <summary>
        /// Отправка кода подтверждения пользователю
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="Code"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<string> SendMessage(string message, string email, CancellationToken cancellation);
    }
}
