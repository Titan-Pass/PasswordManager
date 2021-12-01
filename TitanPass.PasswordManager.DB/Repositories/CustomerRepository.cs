using System;
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

        public Customer GetCustomerByEmail(string email)
        {
            return _ctx.Customers
                .Select(entity => new Customer
                {
                    Id = entity.Id,
                    Email = entity.Email
                }).FirstOrDefault(customer => customer.Email == email);
        }

        public List<Customer> GetAllCustomers()
        {
            return null;
        }

        public Customer CreateCustomer(Customer customer)
        {
            var entity = _ctx.Customers.Add(new CustomerEntity
            {
                Id = customer.Id,
                Email = customer.Email
            }).Entity;

            _ctx.SaveChanges();

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