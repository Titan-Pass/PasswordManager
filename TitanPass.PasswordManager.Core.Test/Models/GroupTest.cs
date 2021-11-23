using TitanPass.PasswordManager.Core.Models;
using Xunit;

namespace TitanPass.PasswordManager.Core.Test.Models
{
    public class GroupTest
    {
        [Fact]
        public void GroupExists()
        {
            var group = new Group();
            Assert.NotNull(group);
        }

        [Fact]
        public void GroupHasStringProperty()
        {
            var group = new Group();
            group.Name = (string) "Test";
            Assert.Equal("Test", group.Name);
        }
    }
}