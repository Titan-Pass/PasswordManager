using TitanPass.PasswordManager.Core.Models;
using Xunit;

namespace TitanPass.PasswordManager.Core.Test.Models
{
    public class AccountTest
    {
        [Fact]
        public void AccountExists()
        {
            var account = new Account();
            Assert.NotNull(account);
        }

        [Fact]
        public void AccountHasStringProperty()
        {
            var account = new Account();
            account.Email = (string) "Test";
            Assert.Equal("Test", account.Email);
        }
     }
}