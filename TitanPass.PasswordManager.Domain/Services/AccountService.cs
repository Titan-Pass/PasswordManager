using System.Collections.Generic;
using System.IO;
using TitanPass.PasswordManager.Core.IServices;
using TitanPass.PasswordManager.Core.Models;
using TitanPass.PasswordManager.Domain.IRepositories;

namespace TitanPass.PasswordManager.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            if (accountRepository == null)
            {
                throw new InvalidDataException("Account repository cannot be null");
            }
            else
            {
                _accountRepository = accountRepository;
            }
        }
        
        public Account GetAccountById(int id)
        {
            return _accountRepository.GetAccountById(id);
        }

        public List<Account> GetAllAccounts()
        {
            return _accountRepository.GetAllAccounts();
        }

        public string GetPassword(int id)
        {
            return _accountRepository.GetPassword(id);
        }

        public List<Account> GetAccountsFromCustomer(int id)
        {
            return _accountRepository.GetAccountsFromCustomer(id);
        }

        public List<Account> GetAccountsFromGroup(int groupId, int customerId)
        {
            return _accountRepository.GetAccountsFromGroup(groupId, customerId);
        }

        public Account CreateAccount(Account account)
        {
            return _accountRepository.CreateAccount(account);
        }

        public Account DeleteAccount(int id)
        {
            return _accountRepository.DeleteAccount(id);
        }

        public Account UpdateAccount(Account account)
        {
            return _accountRepository.UpdateAccount(account);
        }
    }
}