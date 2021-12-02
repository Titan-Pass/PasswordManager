using System.Collections.Generic;
using TitanPass.PasswordManager.Core.Models;

namespace TitanPass.PasswordManager.Domain.IRepositories
{
    public interface IGroupRepository
    {
        Group GetGroupById(int id);
        
        List<Group> GetGroups(int id);

        Group CreateGroup(Group group);
        
        Group DeleteGroup(int id);
        
        Group UpdateGroup(Group group);
    }
}