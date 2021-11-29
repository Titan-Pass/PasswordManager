using TitanPass.PasswordManager.Security.Models;

namespace TitanPass.PasswordManager.Security.IServices
{
    public interface ILoginCustomerService
    {
        LoginCustomer GetCustomerLogin(string email);
        
        LoginCustomer CreateLogin(LoginCustomer loginCustomer);
    }
}