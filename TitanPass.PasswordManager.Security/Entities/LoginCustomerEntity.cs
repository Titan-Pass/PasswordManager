using TitanPass.PasswordManager.Core.Models;

namespace TitanPass.PasswordManager.Security.Entities
{
    public class LoginCustomerEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        //TODO Password should be hashed!
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}