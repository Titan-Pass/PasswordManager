using System;
using TitanPass.PasswordManager.Core.Models;
using TitanPass.PasswordManager.Security.Entities;
using TitanPass.PasswordManager.Security.IServices;

namespace TitanPass.PasswordManager.Security
{
    public class SecurityDbContextSeeder : ISecurityDbContextSeeder
    {
        private readonly ISecurityService _securityService;
        private readonly SecurityDbContext _ctx;

        public SecurityDbContextSeeder(ISecurityService securityService, SecurityDbContext ctx)
        {
            _ctx = ctx;
            _securityService = securityService;
        }
        
        public void SeedDevelopment()
        {
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            
            Byte[] secretBytes = new byte[40];

            using (var rngCsp = new System.Security.Cryptography.RNGCryptoServiceProvider() {})
            {
                rngCsp.GetBytes(secretBytes);
            }

            Customer customer = new Customer
            {
                Id = 1,
                Email = "test@gmail.com"
            };

            _ctx.LoginCustomers.Add(new LoginCustomerEntity
            {
                Id = 1,
                CustomerId = 1,
                Email = "test@gmail.com",
                Salt = secretBytes,
                HashedPassword = _securityService.HashPassword("123456", secretBytes)
            });
            _ctx.SaveChanges();
        }

        public void SeedProduction()
        {
            throw new System.NotImplementedException();
        }
    }
}