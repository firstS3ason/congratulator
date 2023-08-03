
namespace WebApplication5LVL.AppData.Contexts.Mail
{
    public interface IMailService
    {
        /// <summary>
        /// Abstract contract entity to sending info in birthday date to people, by virtual mail
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="Code"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<string> SendMessage(string message, string email, CancellationToken cancellation);
    }
}
