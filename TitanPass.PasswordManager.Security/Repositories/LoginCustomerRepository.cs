using TitanPass.PasswordManager.Security.IRepositories;
using TitanPass.PasswordManager.Security.Models;

namespace TitanPass.PasswordManager.Security.Repositories
{
    public class LoginCustomerRepository : ILoginCustomerRepository
    {
        public LoginCustomer FindByEmailAndPassword(string email, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}