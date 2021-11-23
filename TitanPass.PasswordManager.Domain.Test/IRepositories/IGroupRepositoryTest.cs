using Moq;
using TitanPass.PasswordManager.Domain.IRepositories;
using Xunit;

namespace TitanPass.PasswordManager.Domain.Test.IRepositories
{
    public class IGroupRepositoryTest
    {
        [Fact]
        public void IGroupRepositoryExists()
        {
            var groupRepository = new Mock<IGroupRepository>();
            Assert.NotNull(groupRepository);
        }
    }
}