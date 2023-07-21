using Microsoft.AspNetCore.Http;

namespace WebApplication5LVL.Contracts.User
{
    public sealed class CreateUserRequest
    {
        public string? SFL { get; set; }
        public DateTime birthDay { get; set; }
        public string eMail { get; set; }
        public IFormFile file { get; set; }
    }
}
