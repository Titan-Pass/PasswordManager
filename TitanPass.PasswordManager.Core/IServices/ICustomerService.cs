﻿using System.Collections.Generic;
using TitanPass.PasswordManager.Core.Models;

namespace TitanPass.PasswordManager.Core.IServices
{
    public interface ICustomerService
    {
        Customer GetCustomerById(int id);

        List<Customer> GetAllCustomers();

        Customer CreateCustomer(Customer customer);

        Customer DeleteCustomer(int id);

        Customer UpdateCustomer(Customer customer);
    }
}