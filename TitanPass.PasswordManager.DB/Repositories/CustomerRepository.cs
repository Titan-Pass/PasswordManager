using System.Collections.Generic;
using TitanPass.PasswordManager.Core.Models;
using TitanPass.PasswordManager.DB.Entities;
using TitanPass.PasswordManager.Domain.IRepositories;

namespace TitanPass.PasswordManager.DB.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly PasswordManagerDbContext _ctx;

        public CustomerRepository(PasswordManagerDbContext context)
        {
            _ctx = context;
        }
        
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
            var entity = _ctx.Customers.Remove(new CustomerEntity {Id = id}).Entity;
            _ctx.SaveChanges();
            return new Customer
            {
                Id = entity.Id,
                Email = entity.Email
            };
        }

        public Customer UpdateCustomer(Customer customer)
        {
            var entity = _ctx.Customers.Update(new CustomerEntity
            {
                Email = customer.Email,
                Id = customer.Id
            }).Entity;

            _ctx.SaveChanges();

            return new Customer
            {
                Email = entity.Email,
                Id = entity.Id
            };
        }
    }
}