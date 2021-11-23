using Moq;
using TitanPass.PasswordManager.Domain.IRepositories;
using Xunit;

namespace TitanPass.PasswordManager.Domain.Test.IRepositories
{
    public class IAccountRepositoryTest
    {
        [Fact]
        public void IAccountRepositoryExists()
        {
            var accountRepository = new Mock<IAccountRepository>();
            Assert.NotNull(accountRepository);
        }
    }
}