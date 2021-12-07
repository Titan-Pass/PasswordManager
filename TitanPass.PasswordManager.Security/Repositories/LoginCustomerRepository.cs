using System.Linq;
using System.Text;
using TitanPass.PasswordManager.Security.Entities;
using TitanPass.PasswordManager.Security.IRepositories;
using TitanPass.PasswordManager.Security.Models;

namespace TitanPass.PasswordManager.Security.Repositories
{
    public class LoginCustomerRepository : ILoginCustomerRepository
    {
        private readonly SecurityDbContext _ctx;

        public LoginCustomerRepository(SecurityDbContext context)
        {
            _ctx = context;
        }
        
        public LoginCustomer FindCustomer(string email)
        {
            var entity = _ctx.LoginCustomers
                .FirstOrDefault(customer => email.Equals(customer.Email));
            if (entity == null) return null;
            return new LoginCustomer
            {
                Id = entity.Id,
                Email = entity.Email,
                HashedPassword = entity.HashedPassword,
                Salt = entity.Salt,
                CustomerId = entity.CustomerId
                //Salt = Encoding.ASCII.GetBytes(entity.Salt)
            };
        }

        public LoginCustomer CreateLogin(LoginCustomer loginCustomer)
        {
            var entity = _ctx.LoginCustomers.Add(new LoginCustomerEntity
            {
                Id = loginCustomer.Id,
                Email = loginCustomer.Email,
                HashedPassword = loginCustomer.HashedPassword,
                Salt = loginCustomer.Salt,
                CustomerId = loginCustomer.CustomerId
            }).Entity;

            _ctx.SaveChanges();

            return new LoginCustomer
            {
                Id = entity.Id,
                Email = entity.Email,
                CustomerId = entity.CustomerId,
                HashedPassword = entity.HashedPassword,
                Salt = entity.Salt
            };
        }

        public void UpdateCustomerId(int newId, string email)
        {
            var entity = _ctx.LoginCustomers.FirstOrDefault(customerEntity => customerEntity.Email == email);

            if (entity != null)
            {
                _ctx.LoginCustomers.Update(new LoginCustomerEntity
                {
                    Id = entity.Id,
                    Email = entity.Email,
                    Salt = entity.Salt,
                    HashedPassword = entity.HashedPassword,
                    CustomerId = newId
                });
                _ctx.SaveChanges();
            }
        }

        public LoginCustomer UpdateLoginCustomer(LoginCustomer loginCustomer)
        {
            var entity = _ctx.LoginCustomers.FirstOrDefault(customerEntity =>
                customerEntity.CustomerId == loginCustomer.CustomerId);

            if (entity != null)
            {
                _ctx.LoginCustomers.Update(new LoginCustomerEntity
                {
                    Id = entity.Id,
                    Email = entity.Email,
                    Salt = entity.Salt,
                    HashedPassword = entity.HashedPassword,
                    CustomerId = entity.CustomerId
                });

                _ctx.SaveChanges();
            }

            return new LoginCustomer
            {
                Id = entity.Id,
                Email = entity.Email,
                Salt = entity.Salt,
                HashedPassword = entity.HashedPassword,
                CustomerId = entity.CustomerId
            };
        }
    }
}