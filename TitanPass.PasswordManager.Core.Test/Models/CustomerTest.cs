using TitanPass.PasswordManager.Core.Models;
using Xunit;

namespace TitanPass.PasswordManager.Core.Test.Models
{
    public class CustomerTest
    {
        [Fact]
        public void CustomerExists()
        {
            var customer = new Customer();
            Assert.NotNull(customer);
        }
    }
}