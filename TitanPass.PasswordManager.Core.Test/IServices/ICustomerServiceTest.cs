using Moq;
using TitanPass.PasswordManager.Core.IServices;
using Xunit;

namespace TitanPass.PasswordManager.Core.Test.IServices
{
    public class ICustomerServiceTest
    {
        [Fact]
        public void ICustomerServiceExists()
        {
            var serviceMock = new Mock<ICustomerService>();
            Assert.NotNull(serviceMock);
        }
    }
}