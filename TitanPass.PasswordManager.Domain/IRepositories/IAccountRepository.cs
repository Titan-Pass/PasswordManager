using System.Collections.Generic;
using TitanPass.PasswordManager.Core.Models;

namespace TitanPass.PasswordManager.Domain.IRepositories
{
    public interface IAccountRepository
    {
        Account GetAccountById(int id);

        List<Account> GetAllAccounts();

        Account CreateAccount(Account account);

        Account DeleteAccount(int id);

        Account UpdateAccount(Account account);
    }
}