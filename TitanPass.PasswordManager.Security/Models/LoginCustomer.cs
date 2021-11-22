using TitanPass.PasswordManager.Core.Models;

namespace TitanPass.PasswordManager.Security.Models
{
    public class LoginCustomer
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}