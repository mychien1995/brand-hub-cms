using BrandHub.Data.EF.Databases;
using BrandHub.Data.EF.Entities;
using BrandHub.Framework.IoC;
using BrandHub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrandHub.Data.EF.Repositories.Users
{
    public interface IUserRoleRepository : IEntityRepository<ApplicationUserRole>
    {
        void FetchRoles(UserModel model);
        void AssignRolesToUser(int userId, IEnumerable<int> roleIds);
    }
    [ServiceTypeOf(typeof(IUserRoleRepository))]
    public class UserRoleRepository : BaseRepository<ApplicationUserRole>, IUserRoleRepository
    {
        public UserRoleRepository(BrandHubDbContext context) : base(context)
        {

        }

        public void FetchRoles(UserModel model)
        {
            var roles = this.GetQueryable().AsNoTracking().Include(x => x.Role).Where(x => x.UserId == model.ID);
            model.Roles = roles.Select(x => new Models.Users.RoleModel()
            {
                ID = x.RoleId,
                RoleName = x.Role.RoleName
            }).ToList();
        }

        public void AssignRolesToUser(int userId, IEnumerable<int> roleIds)
        {
            foreach (var roleId in roleIds)
            {
                var hasExistingRole = this.GetQueryable().AsNoTracking().Any(c => c.UserId == userId && c.RoleId == roleId);
                if (hasExistingRole) continue;
                Insert(new ApplicationUserRole()
                {
                    RoleId = roleId,
                    UserId = userId
                });
            }
            SaveChanges();
        }
    }
}
