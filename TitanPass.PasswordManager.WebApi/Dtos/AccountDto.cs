namespace TitanPass.PasswordManager.WebApi.Dtos
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }
        public CustomerDto Customer { get; set; }

        public GroupDto Group { get; set; }
    }
}