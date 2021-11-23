using System.IO;
using Moq;
using TitanPass.PasswordManager.Core.IServices;
using TitanPass.PasswordManager.Domain.IRepositories;
using TitanPass.PasswordManager.Domain.Services;
using Xunit;

namespace TitanPass.PasswordManager.Domain.Test.Services
{
    public class GroupServiceTest
    {
        #region GroupService init

        [Fact]
        public void GroupService_IsIGroupService()
        {
            var repoMock = new Mock<IGroupRepository>();
            var groupService = new GroupService(repoMock.Object);
            Assert.IsAssignableFrom<IGroupService>(groupService);
        }

        [Fact]
        public void GroupService_WithNullRepository_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new GroupService(null));
        }

        [Fact]
        public void GroupService_WithNullRepository_ThrowsExceptionWithMessage()
        {
            var ex = Assert.Throws<InvalidDataException>(() => new GroupService(null));
            Assert.Equal("Group repository cannot be null", ex.Message);
        }

        #endregion
    }
}