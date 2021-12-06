using TitanPass.PasswordManager.Core.Models;

namespace TitanPass.PasswordManager.DB.Entities
{
    public class AccountEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EncryptedPassword { get; set; }
        
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }
        public int GroupId { get; set; }
        public GroupEntity Group { get; set; }
        public string Email { get; set; }
    }
}