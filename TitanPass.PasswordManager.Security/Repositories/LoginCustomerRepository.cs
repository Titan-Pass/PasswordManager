using System.Linq;
using System.Text;
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
                Salt = Encoding.ASCII.GetBytes(entity.Salt)
            };
        }
    }
}