using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TitanPass.PasswordManager.Core.IServices;
using TitanPass.PasswordManager.Core.Models;
using TitanPass.PasswordManager.WebApi.Dtos;

namespace TitanPass.PasswordManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService service)
        {
            _accountService = service;
        }
        
        //Get all accounts
        [HttpGet]
        public ActionResult<AccountDto> GetAllAccounts()
        {
            try
            {
                var accounts = _accountService.GetAllAccounts().Select(account => new AccountDto
                {
                    Id = account.Id,
                    Email = account.Email
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
        public ActionResult<AccountDto> GetAccount(int id)
        {
            var account = _accountService.GetAccountById(id);
            return Ok(new AccountDto
            {
                Email = account.Email,
                Id = account.Id
            });
        }

        
        
        
        
        [HttpDelete("{id}")]
        public Account Delete(int id)
        {
            return _accountService.DeleteAccount(id);
        }

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
                Email = dto.Email
            });
            return Ok(dto);
        }
    }
}