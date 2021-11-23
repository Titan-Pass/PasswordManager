using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TitanPass.PasswordManager.Core.IServices;
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
        
        
    }
}