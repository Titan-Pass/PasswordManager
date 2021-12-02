using System.Collections.Generic;
using System.IO;
using TitanPass.PasswordManager.Core.IServices;
using TitanPass.PasswordManager.Core.Models;
using TitanPass.PasswordManager.Domain.IRepositories;

namespace TitanPass.PasswordManager.Domain.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository repository)
        {
            if (repository == null)
            {
                throw new InvalidDataException("Group repository cannot be null");
            }

            _groupRepository = repository;
        }
        
        public Group GetGroupById(int id)
        {
            return _groupRepository.GetGroupById(id);
        }

        public List<Group> GetGroups(int id)
        {
            return _groupRepository.GetGroups(id);
        }

        public Group CreateGroup(Group @group)
        {
            return _groupRepository.CreateGroup(group);
        }

        public Group DeleteGroup(int id)
        {
            return _groupRepository.DeleteGroup(id);
        }

        public Group UpdateGroup(Group @group)
        {
            return _groupRepository.UpdateGroup(group);
        }
    }
}