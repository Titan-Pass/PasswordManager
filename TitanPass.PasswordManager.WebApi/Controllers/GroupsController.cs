using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public GroupsController(IGroupService service)
        {
            _groupService = service;
        }

        [HttpDelete("{id}")]
        public Group Delete(int id)
        {
            return _groupService.DeleteGroup(id);
        }

        [HttpPut("{id:int}")]
        public ActionResult<GroupDto> UpdateCustomer(int id, GroupDto dto)
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



        /*[HttpGet]
        public ActionResult<GroupsDto> GetAllGroups()
        {
            
        }*/


        [HttpGet("{id:int}")]
        public ActionResult<GroupDto> GetGroupById(int id)
        {
            var group = _groupService.GetGroupById(id);
            return Ok(new GroupDto
            {
                Id = group.Id,
                Name = group.Name
            });
        }
    }
}