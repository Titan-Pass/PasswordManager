using System.Collections.Generic;
using System.IO;
using TitanPass.PasswordManager.Core.IServices;
using TitanPass.PasswordManager.Core.Models;
using TitanPass.PasswordManager.Domain.IRepositories;

namespace TitanPass.PasswordManager.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            if (customerRepository == null)
            {
                throw new InvalidDataException("Customer repository cannot be null");
            }
            else
            {
                _customerRepository = customerRepository;
            }
        }
        
        public Customer GetCustomerById(int id)
        {
            return _customerRepository.GetCustomerById(id);
        }

        public Customer GetCustomerByEmail(string email)
        {
            return _customerRepository.GetCustomerByEmail(email);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAllCustomers();
        }

        public bool CheckIfCustomerExists(string email)
        {
            return _customerRepository.CheckIfCustomerExists(email);
        }

        public Customer CreateCustomer(Customer customer)
        {
            return _customerRepository.CreateCustomer(customer);
        }

        public Customer DeleteCustomer(int id)
        {
            return _customerRepository.DeleteCustomer(id);
        }

        public Customer UpdateCustomer(Customer customer)
        {
            return _customerRepository.UpdateCustomer(customer);
        }
    }
}