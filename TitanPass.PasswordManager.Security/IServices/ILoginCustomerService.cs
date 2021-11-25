using TitanPass.PasswordManager.Security.Models;

namespace TitanPass.PasswordManager.Security.IServices
{
    public interface ILoginCustomerService
    {
        LoginCustomer GetCustomer(string email);
    }
}