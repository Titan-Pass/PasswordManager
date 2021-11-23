using TitanPass.PasswordManager.Security.Models;

namespace TitanPass.PasswordManager.Security.IRepositories
{
    public interface ILoginCustomerRepository
    {
        LoginCustomer FindByEmailAndPassword(string email, string password);
    }
}