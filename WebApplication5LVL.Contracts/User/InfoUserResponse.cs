namespace WebApplication5LVL.Contracts.User
{
    public sealed class InfoUserResponse
    {
        public Guid Id { get; set; }
        public string? SFL { get; set; }
        public DateTime birthDay { get; set; }
        public string eMail { get; set; }
        public byte[] photo { get; set; }
    }
}
