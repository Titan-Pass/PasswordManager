using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TitanPass.PasswordManager.Security.IServices;
using TitanPass.PasswordManager.Security.Models;
using TitanPass.PasswordManager.WebApi.Dtos;

namespace TitanPass.PasswordManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly ILoginCustomerService _loginCustomerService;

        public AuthController(ISecurityService securityService, ILoginCustomerService customerService)
        {
            _securityService = securityService;
            _loginCustomerService = customerService;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public ActionResult<TokenDto> Login(LoginDto dto)
        {
            var token = _securityService.GenerateJwtToken(dto.Email, dto.Password);
            return new TokenDto
            {
                Jwt = token.Jwt,
                Message = token.Message
            };
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<LoginCustomer> CreateLoginCustomer([FromBody] CreateLoginCustomerDto dto)
        {
            Byte[] secretBytes = new byte[40];

            using (var rngCsp = new System.Security.Cryptography.RNGCryptoServiceProvider() {})
            {
                rngCsp.GetBytes(secretBytes);
            }

            var loginCustomerFromDto = new LoginCustomer
            {
                Id = dto.Id,
                Email = dto.Email,
                CustomerId = dto.CustomerId,
                Salt = secretBytes,
                HashedPassword = _securityService.HashPassword(dto.PlainTextPassword, secretBytes)
            };

            try
            {
                var newLoginCustomer = _loginCustomerService.CreateLogin(loginCustomerFromDto);
                return Created($"https://localhost:5001/api/auth/{newLoginCustomer.Id}", newLoginCustomer);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        } 
    }
}