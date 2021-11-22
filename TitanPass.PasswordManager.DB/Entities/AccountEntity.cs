namespace TitanPass.PasswordManager.DB.Entities
{
    public class AccountEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Email { get; set; }
    }
}