namespace TitanPass.PasswordManager.Core.Models
{
    public class Account
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Group Group { get; set; }
        public string Email { get; set; }
    }
}