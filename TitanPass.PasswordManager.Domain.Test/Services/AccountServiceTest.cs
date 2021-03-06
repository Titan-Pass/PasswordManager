using System;
using System.Collections.Generic;
using System.IO;
using Moq;
using TitanPass.PasswordManager.Core.IServices;
using TitanPass.PasswordManager.Core.Models;
using TitanPass.PasswordManager.Domain.IRepositories;
using TitanPass.PasswordManager.Domain.Services;
using Xunit;

namespace TitanPass.PasswordManager.Domain.Test.Services
{
    public class AccountServiceTest
    {
        #region AccountService init
        
        [Fact]
        public void AccountService_IsIAccountService()
        {
            var repoMock = new Mock<IAccountRepository>();
            var accountService = new AccountService(repoMock.Object);
            Assert.IsAssignableFrom<IAccountService>(accountService);
        }
        
        [Fact]
        public void AccountService_WithNullRepository_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new AccountService(null));
        }
        
        [Fact]
        public void AccountService_WithNullRepository_ThrowsExceptionWithMessage()
        {
            var ex = Assert.Throws<InvalidDataException>(() => new AccountService(null));
            Assert.Equal("Account repository cannot be null", ex.Message);
        }
        
        #endregion

        #region AccountService GetAll

        [Fact]
        public void GetAll_NoParams_CallsAccountRepositoryOnce()
        {
            var repoMock = new Mock<IAccountRepository>();
            var productService = new AccountService(repoMock.Object);

            productService.GetAllAccounts();
            
            repoMock.Verify(repository => repository.GetAllAccounts(), Times.Once);
        }
        
        [Fact]
        public void GetAll_NoParams_ReturnsAllAccountsAsList()
        {
            var expected = new List<Account> {new Account {Id = 1, Email = "test"}};
            var repoMock = new Mock<IAccountRepository>();
            repoMock.Setup(repository => repository.GetAllAccounts())
                .Returns(expected);
            var accountService = new AccountService(repoMock.Object);

            accountService.GetAllAccounts();

            Assert.Equal(expected, accountService.GetAllAccounts(), new AccountComparer());
        }

        #endregion

        [Fact]
        public void GetAccountById()
        {
            var expected = new Account
            {
                Id = 1,
                Email = "test@mail.com",
                Name = "test.com"
            };

            var repoMock = new Mock<IAccountRepository>();
            repoMock.Setup(repository => repository.GetAccountById(expected.Id)).Returns(expected);
            var accountService = new AccountService(repoMock.Object);
            int id = 1;
            
            Assert.Equal(expected, accountService.GetAccountById(1), new AccountComparer());
        }
        
        [Fact]
        public void DeleteAccount()
        {
            var account = new Account
            {
                Id = 1,
                Email = "test@mail.com",
                Name = "test.com"
            };

            var repoMock = new Mock<IAccountRepository>();
            repoMock.Setup(repository => repository.DeleteAccount(account.Id));

            AccountService accountService = new AccountService(repoMock.Object);

            accountService.DeleteAccount(account.Id);
            
            repoMock.Verify(repository => repository.DeleteAccount(account.Id));
        }

        [Fact]
        public void CreateAccount()
        {
            var expected = new Account
            {
                Id = 1,
                Name = "test",
                Email = "test@gmail.com"
            };

            var repoMock = new Mock<IAccountRepository>();
            repoMock.Setup(repository => repository.CreateAccount(expected)).Returns(expected);

            AccountService accountService = new AccountService(repoMock.Object);

            accountService.CreateAccount(expected);
            
            Assert.Equal(expected, accountService.CreateAccount(expected), new AccountComparer());
        }
    }

    public class AccountComparer : IEqualityComparer<Account>
    {
        public bool Equals(Account x, Account y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id && x.Email == y.Email;
        }

        public int GetHashCode(Account obj)
        {
            return HashCode.Combine(obj.Id, obj.Email);
        }
    }
}