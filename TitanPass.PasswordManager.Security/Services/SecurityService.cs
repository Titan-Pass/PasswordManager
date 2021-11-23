using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TitanPass.PasswordManager.Security.IServices;
using TitanPass.PasswordManager.Security.Models;

namespace TitanPass.PasswordManager.Security.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly ILoginCustomerService _loginCustomer;
        public IConfiguration Configuration { get; }

        public SecurityService(IConfiguration configuration, ILoginCustomerService loginCustomer)
        {
            Configuration = configuration;
            _loginCustomer = loginCustomer;
        }
        
        public JwtToken GenerateJwtToken(string email, string password)
        {
            var loginCustomer = _loginCustomer.Login(email, password);
            //Validate User - Generate
            if (loginCustomer != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(Configuration["Jwt:Issuer"],
                    Configuration["Jwt:Audience"],
                    null,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: credentials);
                return new JwtToken
                {
                    Jwt = new JwtSecurityTokenHandler().WriteToken(token),
                    Message = "Ok"
                };
            }
            return new JwtToken
            {
                Message = "User or Password not correct"
            };
        }
    }
}