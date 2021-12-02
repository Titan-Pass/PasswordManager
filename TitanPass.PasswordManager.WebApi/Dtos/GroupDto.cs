using TitanPass.PasswordManager.Core.Models;

namespace TitanPass.PasswordManager.WebApi.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Customer Customer { get; set; }
    }
}