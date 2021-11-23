using System.Collections.Generic;
using System.Linq;
using TitanPass.PasswordManager.Core.Models;
using TitanPass.PasswordManager.DB.Entities;
using TitanPass.PasswordManager.Domain.IRepositories;

namespace TitanPass.PasswordManager.DB.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly PasswordManagerDbContext _ctx;
        
        public CustomerRepository(PasswordManagerDbContext ctx)
        {
            _ctx = ctx;
        }
        
        public Customer GetCustomerById(int id)
        {
            return _ctx.Customers
                .Select(entity => new Customer
                {
                    Id = entity.Id,
                    Email = entity.Email
                }).FirstOrDefault(customer => customer.Id == id);
        }

        public List<Customer> GetAllCustomers()
        {
            throw new System.NotImplementedException();
        }

        public Customer CreateCustomer(Customer customer)
        {
            var entity = _ctx.Customers.Add(new CustomerEntity
            {
                Id = customer.Id,
                Email = customer.Email
            }).Entity;

            return new Customer
            {
                Id = entity.Id,
                Email = entity.Email
            };
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
            throw new System.NotImplementedException();
        }
    }
}