namespace TitanPass.PasswordManager.WebApi.Dtos
{
    public class UpdatePasswordDto
    {
        public int Id { get; set; }
        public string PlainTextPassword { get; set; }
    }
}