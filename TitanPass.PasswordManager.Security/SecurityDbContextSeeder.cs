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

            var salt1 = _securityService.GenerateSalt();
            
            _ctx.LoginCustomers.Add(new LoginCustomerEntity
            {
                Id = 1,
                CustomerId = 1,
                Email = "test@gmail.com",
                Salt = Convert.ToBase64String(salt1),
                HashedPassword = _securityService.HashPassword("123456", salt1)
            });

            _ctx.SaveChanges();
            
            var salt2 = _securityService.GenerateSalt();

            _ctx.LoginCustomers.Add(new LoginCustomerEntity
            {
                Id = 2,
                CustomerId = 2,
                Email = "test2@hotmail.com",
                Salt = Convert.ToBase64String(salt2),
                HashedPassword = _securityService.HashPassword("654321", salt2)
            });
            
            _ctx.SaveChanges();
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureCreated();
            
            var salt1 = _securityService.GenerateSalt();
            
            _ctx.LoginCustomers.Add(new LoginCustomerEntity
            {
                Id = 1,
                CustomerId = 1,
                Email = "test@gmail.com",
                Salt = Convert.ToBase64String(salt1),
                HashedPassword = _securityService.HashPassword("123456", salt1)
            });

            var salt2 = _securityService.GenerateSalt();

            _ctx.LoginCustomers.Add(new LoginCustomerEntity
            {
                Id = 2,
                CustomerId = 2,
                Email = "test2@hotmail.com",
                Salt = Convert.ToBase64String(salt2),
                HashedPassword = _securityService.HashPassword("654321", salt2)
            });
            
            _ctx.SaveChanges();
        }
    }
}