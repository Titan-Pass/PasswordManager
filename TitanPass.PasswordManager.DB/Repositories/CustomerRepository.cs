using System.Collections.Generic;
using TitanPass.PasswordManager.Core.Models;
using TitanPass.PasswordManager.Domain.IRepositories;

namespace TitanPass.PasswordManager.DB.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public Customer GetCustomerById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Customer> GetAllCustomers()
        {
            throw new System.NotImplementedException();
        }

        public Customer CreateCustomer(Customer customer)
        {
            throw new System.NotImplementedException();
        }

        public Customer DeleteCustomer(int id)
        {
            throw new System.NotImplementedException();
        }

        public Customer UpdateCustomer(Customer customer)
        {
            throw new System.NotImplementedException();
        }
    }
}