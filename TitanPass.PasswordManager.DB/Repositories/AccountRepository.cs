﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TitanPass.PasswordManager.Core.Models;
using TitanPass.PasswordManager.DB.Entities;
using TitanPass.PasswordManager.Domain.IRepositories;

namespace TitanPass.PasswordManager.DB.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly PasswordManagerDbContext _ctx;

        public AccountRepository(PasswordManagerDbContext ctx)
        {
            _ctx = ctx;
        }
        
        public Account GetAccountById(int id)
        {
            return _ctx.Accounts.Select(entity => new Account
            {
                Id = entity.Id,
                Email = entity.Email
            }).FirstOrDefault(account => account.Id == id);
        }

        public List<Account> GetAllAccounts()
        {
            throw new System.NotImplementedException();
        }

        public List<Account> GetAccountsFromCustomer(int id)
        {
            throw new System.NotImplementedException();
        }

        public Account CreateAccount(Account account)
        {
            throw new System.NotImplementedException();
        }

        public Account DeleteAccount(int id)
        {
            var entity = _ctx.Accounts.Remove(new AccountEntity {Id = id}).Entity;
            _ctx.SaveChanges();
            return new Account
            {
                Id = entity.Id,
                Customer = entity.Customer,
                Email = entity.Email,
                Group = entity.Group
            };
        }

        public Account UpdateAccount(Account account)
        {
            throw new System.NotImplementedException();
        }
    }
}