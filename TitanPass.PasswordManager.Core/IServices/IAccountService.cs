using System.Collections.Generic;
using TitanPass.PasswordManager.Core.Models;

namespace TitanPass.PasswordManager.Core.IServices
{
    public interface IAccountService
    {
        Account GetAccountById(int id);

        List<Account> GetAllAccounts();

        string GetPassword(int id);

        List<Account> GetAccountsFromCustomer(int id);

        List<Account> GetAccountsFromGroup(int groupId, int customerId);

        Account CreateAccount(Account account);

        Account DeleteAccount(int id);

        Account UpdateAccount(Account account);
    }
}