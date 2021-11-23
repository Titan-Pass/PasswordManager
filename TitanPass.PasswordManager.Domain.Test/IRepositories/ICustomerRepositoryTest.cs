using Moq;
using TitanPass.PasswordManager.Domain.IRepositories;
using Xunit;

namespace TitanPass.PasswordManager.Domain.Test.IRepositories
{
    public class ICustomerRepositoryTest
    {
        [Fact]
        public void ICustomerRepositoryExists()
        {
            var customerRepository = new Mock<ICustomerRepository>();
            Assert.NotNull(customerRepository);
        }
    }
}