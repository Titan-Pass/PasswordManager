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