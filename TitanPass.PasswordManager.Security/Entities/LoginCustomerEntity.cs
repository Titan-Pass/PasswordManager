using System;
using Microsoft.EntityFrameworkCore.Metadata;
using TitanPass.PasswordManager.Core.Models;

namespace TitanPass.PasswordManager.Security.Entities
{
    public class LoginCustomerEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public Byte[] Salt { get; set; }
        public int CustomerId { get; set; }
    }
}