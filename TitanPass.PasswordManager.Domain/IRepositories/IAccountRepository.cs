using System.Collections.Generic;
using TitanPass.PasswordManager.Core.Models;

namespace TitanPass.PasswordManager.Domain.IRepositories
{
    public interface IAccountRepository
    {
        Account GetAccountById(int id);

        List<Account> GetAllAccounts();
        
        List<Account> GetAccountsFromCustomer(int id);
        
        string GetPassword(int id);

        Account CreateAccount(Account account);

        Account DeleteAccount(int id);

        Account UpdateAccount(Account account);
    }
}