using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TitanPass.PasswordManager.WebApi.Dtos
{
    public class CustomersDto
    {
        public List<CustomerDto> List { get; set; }
    }
}