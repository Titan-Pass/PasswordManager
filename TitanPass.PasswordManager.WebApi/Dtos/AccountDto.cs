namespace TitanPass.PasswordManager.WebApi.Dtos
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string EncryptedPassword { get; set; }
        public string MasterPassword { get; set; }
        public CustomerDto Customer { get; set; }
        public GroupDto Group { get; set; }
    }
}