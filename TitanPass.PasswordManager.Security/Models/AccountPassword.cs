using TitanPass.PasswordManager.Core.Models;

namespace TitanPass.PasswordManager.Security.Models
{
    public class AccountPassword
    {
        public int Id { get; set; }
        public string EncryptedPassword { get; set; }
        public Account Account { get; set; }
        public int AccountId { get; set; }
        public string Salt { get; set; }
        
    }
}