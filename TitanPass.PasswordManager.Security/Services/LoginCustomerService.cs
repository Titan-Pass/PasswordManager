using TitanPass.PasswordManager.Security.IRepositories;
using TitanPass.PasswordManager.Security.IServices;
using TitanPass.PasswordManager.Security.Models;

namespace TitanPass.PasswordManager.Security.Services
{
    public class LoginCustomerService : ILoginCustomerService
    {
        private readonly ILoginCustomerRepository _customerRepository;

        public LoginCustomerService(ILoginCustomerRepository loginCustomerRepository)
        {
            _customerRepository = loginCustomerRepository;
        }
        
        public LoginCustomer GetCustomer(string email)
        {
            return _customerRepository.FindCustomer(email);
        }
    }
}