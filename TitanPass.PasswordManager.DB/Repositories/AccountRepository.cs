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
                Name = entity.Name,
                Email = entity.Email,
                Password = entity.EncryptedPassword,
                Customer = new Customer
                {
                    Id = entity.Customer.Id,
                    Email = entity.Customer.Email
                },
                Group = new Group
                {
                    Id = entity.Group.Id,
                    Name = entity.Group.Name
                }
            }).FirstOrDefault(account => account.Id == id);
        }

        public List<Account> GetAllAccounts()
        {
            return _ctx.Accounts.Select(entity => new Account
            {
                Id = entity.Id,
                Email = entity.Email,
                Name = entity.Name,
                Group = new Group
                {
                    Id = entity.Group.Id,
                    Name = entity.Group.Name
                },
                Customer = new Customer
                {
                    Id = entity.Customer.Id,
                    Email = entity.Customer.Email
                }
            }).ToList();
        }

        public List<Account> GetAccountsFromCustomer(int id)
        {
            return _ctx.Accounts.Select(entity => new Account
            {
                Id = entity.Id,
                Email = entity.Email,
                Name = entity.Name,
                Group = new Group
                {
                    Id = entity.Group.Id,
                    Name = entity.Group.Name
                },
                Customer = new Customer
                {
                    Id = entity.Customer.Id,
                    Email = entity.Customer.Email
                }
            }).Where(account => account.Customer.Id == id).ToList();
        }

        public List<Account> GetAccountsFromGroup(int groupId, int customerId)
        {
            return _ctx.Accounts.Select(entity => new Account
            {
                Id = entity.Id,
                Email = entity.Email,
                Name = entity.Name,
                Group = new Group
                {
                    Id = entity.Group.Id,
                    Name = entity.Group.Name
                },
                Customer = new Customer
                {
                    Id = entity.Customer.Id,
                    Email = entity.Customer.Email
                }
            }).Where(account => account.Group.Id == groupId && account.Customer.Id == customerId).ToList();
        }

        public string GetPassword(int id)
        {
            var entity = _ctx.Accounts.Select(accountEntity => new Account
            {
                Password = accountEntity.EncryptedPassword
            }).FirstOrDefault(account => account.Id == id);

            
            return entity.Password;
        }

        public Account CreateAccount(Account account)
        {
            var entity = _ctx.Accounts.Add(new AccountEntity
            {
                Id = account.Id,
                Name = account.Name,
                Email = account.Email,
                EncryptedPassword = account.Password,
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
                Email = entity.Email,
            };
        }

        public Account UpdateAccount(Account account)
        {
            var entity = _ctx.Accounts.Update(new AccountEntity
            {
                Id = account.Id,
                Name = account.Name,
                Email = account.Email,
                EncryptedPassword = account.Password,
                CustomerId = account.Customer.Id,
                GroupId = account.Group.Id
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