using System.Collections.Generic;
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





        //Read all Accounts
        public List<Account> ReadAll()
        {
            return _ctx.Accounts.Select(entity => new Account
            {
                Id = entity.Id,
                Customer = entity.Customer,
                Email = entity.Email,
                Group = entity.Group,
                Name = entity.Name
            }).ToList();
        }
        
        //Get single account by ID
        public Account GetAccount(int id)
        {
            return _ctx.Accounts.Select(entity => new Account
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Customer = entity.Customer,
                Group = entity.Group
            }).FirstOrDefault(account => account.Id == id);
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        public Account CreateAccount(Account account)
        {
            var entity = _ctx.Accounts.Add(new AccountEntity
            {
                Id = account.Id,
                Name = account.Name,
                Email = account.Email,
                CustomerId = account.Customer.Id,
                GroupId = account.Group.Id
            }).Entity;

            _ctx.SaveChanges();

            return new Account
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
            };
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
            var entity = _ctx.Accounts.Update(new AccountEntity
            {
                Id = account.Id,
                Name = account.Name,
                Email = account.Email
            }).Entity;

            _ctx.SaveChanges();

            return new Account
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email
            };
        }
    }
}