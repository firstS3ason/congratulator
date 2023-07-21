namespace WebApplication5LVL.Contracts.User
{
    public sealed class UpdateUserRequest
    {
        public string? SFL { get; set; }
        public DateTime birthDay { get; set; }
        public byte[] photo { get; set; }
        public string eMail { get; set; }
    }
}
