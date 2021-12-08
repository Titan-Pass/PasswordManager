using TitanPass.PasswordManager.Security.Models;

namespace TitanPass.PasswordManager.Security.IServices
{
    public interface ILoginCustomerService
    {
        LoginCustomer GetCustomerLogin(string email);
        
        LoginCustomer CreateLogin(LoginCustomer loginCustomer);

        void UpdateCustomerId(int id, string email);

        LoginCustomer UpdateLoginCustomer(LoginCustomer loginCustomer);

        void UpdatePassword(LoginCustomer loginCustomer);
    }
}