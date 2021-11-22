using Moq;
using TitanPass.PasswordManager.Core.IServices;
using Xunit;

namespace TitanPass.PasswordManager.Core.Test.IServices
{
    public class IAccountServiceTest
    {
        [Fact]
        public void IAccountServiceExists()
        {
            var serviceMock = new Mock<IAccountService>();
            Assert.NotNull(serviceMock);
        }
    }
}