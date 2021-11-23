using TitanPass.PasswordManager.Security.Models;

namespace TitanPass.PasswordManager.Security.IServices
{
    public interface ILoginCustomerService
    {
        LoginCustomer Login(string email, string password);
    }
}