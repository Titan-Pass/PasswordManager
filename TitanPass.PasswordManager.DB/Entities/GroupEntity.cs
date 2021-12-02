using TitanPass.PasswordManager.Core.Models;

namespace TitanPass.PasswordManager.DB.Entities
{
    public class GroupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CustomerEntity Customer { get; set; }
        public int CustomerId { get; set; }
    }
}