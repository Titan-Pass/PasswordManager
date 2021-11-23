using Moq;
using TitanPass.PasswordManager.Core.IServices;
using Xunit;

namespace TitanPass.PasswordManager.Core.Test.IServices
{
    public interface IGroupServiceTest
    {
        [Fact]
        public void IGroupServiceExists()
        {
            var groupService = new Mock<IGroupService>();
            Assert.NotNull(groupService);
        }
    }
}