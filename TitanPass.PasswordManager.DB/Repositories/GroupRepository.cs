using System.Collections.Generic;
using System.Linq;
using TitanPass.PasswordManager.Core.Models;
using TitanPass.PasswordManager.DB.Entities;
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
            return _ctx.Groups.Select(entity => new Group
            {
                Id = entity.Id,
                Name = entity.Name
            }).FirstOrDefault(group => group.Id == id);
        }

        public List<Group> GetAllGroups()
        {
            return _ctx.Groups.Select(entity => new Group
            {
                Id = entity.Id,
                Name = entity.Name
            }).ToList();
        }

        public Group CreateGroup(Group @group)
        {
            var entity = _ctx.Groups.Add(new GroupEntity
            {
                Id = group.Id,
                Name = group.Name,
                CustomerId = group.Customer.Id
            }).Entity;

            _ctx.SaveChanges();

            return new Group
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public Group DeleteGroup(int id)
        {
            var entity = _ctx.Groups.Remove(new GroupEntity {Id = id}).Entity;
            _ctx.SaveChanges();
            return new Group
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public Group UpdateGroup(Group @group)
        {
            var entity = _ctx.Groups.Update(new GroupEntity
            {
                Id = group.Id,
                Name = group.Name
            }).Entity;

            _ctx.SaveChanges();
            return new Group
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}