using TitanPass.PasswordManager.Security.Models;

namespace TitanPass.PasswordManager.Security.IRepositories
{
    public interface ILoginCustomerRepository
    {
        LoginCustomer FindCustomer(string email);

        LoginCustomer CreateLogin(LoginCustomer loginCustomer);
        
        void UpdateCustomerId(int newId, string email);
        
        LoginCustomer UpdateLoginCustomer(LoginCustomer loginCustomer);
    }
}