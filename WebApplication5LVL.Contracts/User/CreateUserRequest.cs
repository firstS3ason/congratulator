using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5LVL.Contracts.User
{
    public sealed class CreateUserRequest
    {
        public string? SFL { get; set; }
        public DateTime birthDay { get; set; }
        [EmailAddress()]
        public string eMail { get; set; }
        public IFormFile file { get; set; }
    }
}
