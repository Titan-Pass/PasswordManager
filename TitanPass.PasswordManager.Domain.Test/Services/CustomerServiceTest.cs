using System.IO;
using Moq;
using TitanPass.PasswordManager.Core.IServices;
using TitanPass.PasswordManager.Domain.IRepositories;
using TitanPass.PasswordManager.Domain.Services;
using Xunit;

namespace TitanPass.PasswordManager.Domain.Test.Services
{
    public class CustomerServiceTest
    {
        #region CustomerService init

        [Fact]
        public void CustomerService_IsICustomerService()
        {
            var repoMock = new Mock<ICustomerRepository>();
            var customerService = new CustomerService(repoMock.Object);
            Assert.IsAssignableFrom<ICustomerService>(customerService);
        }
        
        [Fact]
        public void CustomerService_WithNullRepository_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new CustomerService(null));
        }
        
        [Fact]
        public void CustomerService_WithNullRepository_ThrowsExceptionWithMessage()
        {
            var ex = Assert.Throws<InvalidDataException>(() => new CustomerService(null));
            Assert.Equal("Customer repository cannot be null", ex.Message);
        }

        #endregion
    }
}