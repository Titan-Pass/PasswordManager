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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService service)
        {
            _customerService = service;
        }

        [HttpDelete("{id}")]
        public Customer Delete(int id)
        {
            return _customerService.DeleteCustomer(id);
        }
        
        [HttpPut("{id:int}")]
        public ActionResult<CustomerDto> UpdateCustomer(int id, CustomerDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("It is not a match");
            }

            var customer = _customerService.UpdateCustomer(new Customer
            {
                Id = dto.Id,
                Email = dto.Email
            });
            return Ok(dto);
        }
    }
}