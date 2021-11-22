using System.Collections.Generic;
using TitanPass.PasswordManager.Core.Models;
using TitanPass.PasswordManager.Domain.IRepositories;

namespace TitanPass.PasswordManager.DB.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Account GetAccountById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Account> GetAllAccounts()
        {
            throw new System.NotImplementedException();
        }

        public Account CreateAccount(Account account)
        {
            throw new System.NotImplementedException();
        }

        public Account DeleteAccount(int id)
        {
            throw new System.NotImplementedException();
        }

        public Account UpdateAccount(Account account)
        {
            throw new System.NotImplementedException();
        }
    }
}