using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TitanPass.PasswordManager.Core.IServices;
using TitanPass.PasswordManager.Core.Models;
using TitanPass.PasswordManager.Security.IServices;
using TitanPass.PasswordManager.Security.Models;
using TitanPass.PasswordManager.WebApi.Dtos;

namespace TitanPass.PasswordManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILoginCustomerService _loginCustomerService;
        private IEncryptionService _encryptionService;
        private readonly ISecurityService _securityService;

        public AccountsController(IAccountService service, ILoginCustomerService customerService, IEncryptionService encryptionService, ISecurityService securityService)
        {
            _accountService = service;
            _loginCustomerService = customerService;
            _encryptionService = encryptionService;
            _securityService = securityService;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<AccountDto> CreateAccount([FromBody] AccountDto dto)
        {
            string currentCustomerEmail = User.FindFirstValue(ClaimTypes.Email);
            LoginCustomer customer = _loginCustomerService.GetCustomerLogin(currentCustomerEmail);

            if (_securityService.Authenticate(dto.MasterPassword, customer))
            {
                var accountFromDto = new Account
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Email = dto.Email,
                    Password = _encryptionService.EncryptPassword(dto.EncryptedPassword, dto.MasterPassword + customer.Salt),
                    Customer = new Customer
                    {
                        Id = customer.CustomerId,
                        Email = customer.Email
                    },
                    Group = new Group
                    {
                        Id = dto.Group.Id,
                        Name = dto.Group.Name
                    }
                };
                try
                {
                    var newAccount = _accountService.CreateAccount(accountFromDto);
                    return Created($"https://localhost:5001/api/accounts/{newAccount.Id}", newAccount);
                }
                catch (ArgumentException ae)
                {
                    return BadRequest(ae.Message);
                }
            }

            return BadRequest("Could not create new account");
        }

        [HttpGet]
        [Authorize]
        public ActionResult<AccountsDto> GetAccounts()
        {
            string currentCustomerEmail = User.FindFirstValue(ClaimTypes.Email);
            LoginCustomer customer = _loginCustomerService.GetCustomerLogin(currentCustomerEmail);
            
            try
            {
                var accounts = _accountService.GetAccountsFromCustomer(customer.CustomerId).Select(account => new AccountDto
                {
                    Id = account.Id,
                    Email = account.Email,
                    Name = account.Name,
                    EncryptedPassword = account.Password,
                    Customer = new CustomerDto
                    {
                        Id = account.Customer.Id,
                        Email = account.Customer.Email
                    },
                    Group = new GroupDto
                    {
                        Id = account.Group.Id,
                        Name = account.Group.Name
                    }
                }).ToList();

                return Ok(new AccountsDto
                {
                    List = accounts
                });

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize]
        [HttpGet("decrypt/{id}/{password}")]
        public ActionResult<PasswordDto> GetPassword(string password, int id)
        {
            string currentCustomerEmail = User.FindFirstValue(ClaimTypes.Email);
            LoginCustomer customer = _loginCustomerService.GetCustomerLogin(currentCustomerEmail);

            var account = _accountService.GetAccountById(id);

            var decryptedPassword = _encryptionService.DecryptPassword(account.Password, password + customer.Salt);

            var passwordDto = new PasswordDto
            {
                plainTextPassword = decryptedPassword
            };

            return Ok(passwordDto);
        }
        
        //Get single account by Id
        [HttpGet("{id}")]
        public ActionResult<AccountDto> GetAccount(int id)
        {
            var account = _accountService.GetAccountById(id);
            return Ok(new AccountDto
            {
                Email = account.Email,
                Id = account.Id,
                Name = account.Name,
                Customer = new CustomerDto
                {
                    Id = account.Customer.Id,
                    Email = account.Customer.Email
                },
                Group = new GroupDto
                {
                    Id = account.Group.Id,
                    Name = account.Group.Name
                }
            });
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public Account Delete(int id)
        {
            return _accountService.DeleteAccount(id);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public ActionResult<AccountDto> UpdateAccount(int id, AccountDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("It is not a match");
            }

            var account = _accountService.UpdateAccount(new Account
            {
                Id = dto.Id,
                Email = dto.Email,
                Name = dto.Name,
                Customer = new Customer
                {
                    Email = dto.Customer.Email,
                    Id = dto.Customer.Id
                },
                Group = new Group
                {
                    Id = dto.Group.Id,
                    Name = dto.Group.Name
                }
            });
            return Ok(dto);
        }
    }
}