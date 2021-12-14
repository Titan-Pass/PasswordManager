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
        private readonly IGroupService _groupService;
        private readonly ICustomerService _customerService;

        public AccountsController(IAccountService service, ILoginCustomerService customerService, IEncryptionService encryptionService, ISecurityService securityService, IGroupService groupService, ICustomerService cService)
        {
            _accountService = service;
            _loginCustomerService = customerService;
            _encryptionService = encryptionService;
            _securityService = securityService;
            _groupService = groupService;
            _customerService = cService;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<AccountDto> CreateAccount([FromBody] AccountDto dto)
        {
            string currentCustomerEmail = User.FindFirstValue(ClaimTypes.Email);
            LoginCustomer customer = _loginCustomerService.GetCustomerLogin(currentCustomerEmail);
            Group @group = _groupService.GetGroupById(dto.GroupId);

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
                        Id = group.Id,
                        Name = group.Name
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
        [HttpPost("decrypt")]
        public ActionResult<PasswordDto> GetPassword([FromBody] PasswordDto dto)
        {
            string currentCustomerEmail = User.FindFirstValue(ClaimTypes.Email);
            LoginCustomer customer = _loginCustomerService.GetCustomerLogin(currentCustomerEmail);

            var account = _accountService.GetAccountById(dto.Id);

            var decryptedPassword = _encryptionService.DecryptPassword(account.Password, dto.password + customer.Salt);

            var passwordDto = new PasswordDto
            {
                password = decryptedPassword
            };

            return Ok(passwordDto);
        }

        [HttpGet("groups/{id}")]
        [Authorize]
        public ActionResult<AccountsDto> GetAccountsFromGroups(int id)
        {
            string currentCustomerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int customerId = Int32.Parse(currentCustomerId);
            Customer customer = _customerService.GetCustomerById(customerId);
            
            try
            {
                var accounts = _accountService.GetAccountsFromGroup(id, customer.Id).Select(account => new AccountDto
                {
                    Id = account.Id,
                    Name = account.Name,
                    Email = account.Email,
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

        //Get single account by Id
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<AccountDto> GetAccount(int id)
        {
            string currentCustomerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int customerId = Int32.Parse(currentCustomerId);

            var account = _accountService.GetAccountById(id);

            if (account.Customer.Id == customerId)
            {
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

            return BadRequest("Ids dont match");
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
            string currentCustomerEmail = User.FindFirstValue(ClaimTypes.Email);
            LoginCustomer customer = _loginCustomerService.GetCustomerLogin(currentCustomerEmail);
            Group @group = _groupService.GetGroupById(dto.GroupId);
            
            if (id != dto.Id)
            {
                return BadRequest("It is not a match");
            }

            var account = _accountService.UpdateAccount(new Account
            {
                Id = dto.Id,
                Email = dto.Email,
                Name = dto.Name,
                Password = _encryptionService.EncryptPassword(dto.EncryptedPassword, dto.MasterPassword + customer.Salt),
                Customer = new Customer
                {
                    Email = customer.Email,
                    Id = customer.CustomerId
                },
                Group = new Group
                {
                    Id = group.Id,
                    Name = group.Name
                }
            });
            return Ok(dto);
        }
    }
}