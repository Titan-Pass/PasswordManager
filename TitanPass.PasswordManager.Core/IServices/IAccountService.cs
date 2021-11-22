using System.Collections.Generic;
using TitanPass.PasswordManager.Core.Models;

namespace TitanPass.PasswordManager.Core.IServices
{
    public interface IAccountService
    {
        Account GetAccountById(int id);

        List<Account> GetAllAccounts();

        List<Account> GetAccountsFromCustomer(int id);

        Account CreateAccount(Account account);

        Account DeleteAccount(int id);

        Account UpdateAccount(Account account);
    }
}