using System;

namespace TitanPass.PasswordManager.WebApi.Dtos
{
    public class CreateLoginCustomerDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PlainTextPassword { get; set; }
        public int CustomerId { get; set; }
    }
}