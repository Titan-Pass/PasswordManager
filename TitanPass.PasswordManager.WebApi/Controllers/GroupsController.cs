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
using TitanPass.PasswordManager.WebApi.Dtos;
using Group = TitanPass.PasswordManager.Core.Models.Group;

namespace TitanPass.PasswordManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly ICustomerService _customerService;

        public GroupsController(IGroupService service, ICustomerService customerService)
        {
            _groupService = service;
            _customerService = customerService;
        }

        [HttpDelete("{id}")]
        public Group Delete(int id)
        {
            return _groupService.DeleteGroup(id);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public ActionResult<GroupDto> UpdateGroup(int id, GroupDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("It is not a match");
            }

            var group = _groupService.UpdateGroup(new Group
            {
                Id = dto.Id,
                Name = dto.Name
            });
            return Ok(dto);
        }

        [HttpPost]
        [Authorize]
        public ActionResult<GroupDto> CreateGroup([FromBody] GroupDto dto)
        {
            string currentCustomerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int customerId = Int32.Parse(currentCustomerId);
            Customer customer = _customerService.GetCustomerById(customerId);

            var groupFromDto = new Group
            {
                Id = dto.Id,
                Name = dto.Name,
                Customer = new Customer
                {
                    Id = customer.Id,
                    Email = customer.Email
                }
            };

            try
            {
                var newGroup = _groupService.CreateGroup(groupFromDto);
                return Created($"https://localhost:5001/api/groups/{newGroup.Id}", newGroup);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult<GroupsDto> GetGroups()
        {
            string currentCustomerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int customerId = Int32.Parse(currentCustomerId);
            Customer customer = _customerService.GetCustomerById(customerId);
            
            try
            {
                var groups = _groupService.GetGroups(customer.Id)
                    .Select(group => new GroupDto
                    {
                        Id = group.Id,
                        Name = group.Name,
                        Customer = new Customer
                        {
                            Id = group.Customer.Id,
                            Email = group.Customer.Email
                        }
                    }).ToList();
                return Ok(new GroupsDto
                {
                    List = groups
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet("{id:int}")]
        public ActionResult<GroupDto> GetGroupById(int id)
        {
            var group = _groupService.GetGroupById(id);
            return Ok(new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
            });
        }
    }
}