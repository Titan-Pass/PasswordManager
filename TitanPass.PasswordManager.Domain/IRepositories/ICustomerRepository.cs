using System.Collections.Generic;
using TitanPass.PasswordManager.Core.Models;

namespace TitanPass.PasswordManager.Domain.IRepositories
{
    public interface ICustomerRepository
    {
        Customer GetCustomerById(int id);
        
        Customer GetCustomerByEmail(string email);

        List<Customer> GetAllCustomers();
        bool CheckIfCustomerExists(string email);

        Customer CreateCustomer(Customer customer);

        Customer DeleteCustomer(int id);

        Customer UpdateCustomer(Customer customer);
    }
}