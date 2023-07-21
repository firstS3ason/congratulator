namespace WebApplication5LVL.Domain.Models
{
    /// <summary>
    /// Concrete model - User.
    /// In logical "IS A" relationship with - abstract Entity
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// User's - Surname;FirstName;LastName in string constant
        /// </summary>
        public string? SFL { get; set; }
        /// <summary>
        /// User's birthday
        /// </summary>
        public DateTime birthDay { get; set; }
        /// <summary>
        /// User's photo
        /// </summary>
        public byte[] photo { get; set; }
        /// <summary>
        /// User's email
        /// </summary>
        public string eMail { get; set; }
    }
}
