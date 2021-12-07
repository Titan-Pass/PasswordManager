namespace TitanPass.PasswordManager.WebApi.Dtos
{
    public class LoginCustomerDto
    {
        public int Id { get; set; }
        public string PlainTextPassword { get; set; }
        public string Email { get; set; }
        public int CustomerId { get; set; }
    }
}