namespace TitanPass.PasswordManager.Core.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Customer Customer { get; set; }
    }
}