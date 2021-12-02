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
        
        public LoginCustomer GetCustomerLogin(string email)
        {
            return _customerRepository.FindCustomer(email);
        }

        public LoginCustomer CreateLogin(LoginCustomer loginCustomer)
        {
            return _customerRepository.CreateLogin(loginCustomer);
        }

        public void UpdateCustomerId(int newId, string email)
        {
            _customerRepository.UpdateCustomerId(newId, email);
        }
    }
}