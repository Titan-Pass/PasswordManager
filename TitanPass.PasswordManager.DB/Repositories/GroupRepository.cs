using System.Collections.Generic;
using TitanPass.PasswordManager.Core.Models;
using TitanPass.PasswordManager.Domain.IRepositories;

namespace TitanPass.PasswordManager.DB.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly PasswordManagerDbContext _ctx;

        public GroupRepository(PasswordManagerDbContext ctx)
        {
            _ctx = ctx;
        }
        
        public Group GetGroupById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Group> GetAllGroups()
        {
            throw new System.NotImplementedException();
        }

        public Group CreateGroup(Group @group)
        {
            throw new System.NotImplementedException();
        }

        public Group DeleteGroup(int id)
        {
            throw new System.NotImplementedException();
        }

        public Group UpdateGroup(Group @group)
        {
            throw new System.NotImplementedException();
        }
    }
}