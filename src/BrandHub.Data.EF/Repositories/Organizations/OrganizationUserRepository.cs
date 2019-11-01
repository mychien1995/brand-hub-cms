using BrandHub.Data.EF.Databases;
using BrandHub.Data.EF.Entities;
using BrandHub.Framework.IoC;
using BrandHub.Models;
using BrandHub.Models.Organizations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandHub.Data.EF.Repositories.Organizations
{
    public interface IOrganizationUserRepository : IEntityRepository<OrganizationUser>
    {
        bool UserIsInOrganization(int userId, int organizationId);
        Task<bool> UserIsInOrganizationAsync(int userId, int organizationId);
        void AssignUserToOrganization(OrganizationUserRoleUpdateModel updateModel);
    }

    [ServiceTypeOf(typeof(IOrganizationUserRepository))]
    public class OrganizationUserRepository : BaseRepository<OrganizationUser>, IOrganizationUserRepository
    {
        public OrganizationUserRepository(BrandHubDbContext context) : base(context)
        {

        }

        public void AssignUserToOrganization(OrganizationUserRoleUpdateModel updateModel)
        {
            var organizationId = updateModel.OrganizationId;
            foreach (var user in updateModel.Users)
            {
                var oldRoles = this.GetQueryable().Where(x => x.OrganizationId == organizationId && x.UserId == user.UserId);
                var rolesToDelete = oldRoles.Where(x => !user.OrganizationRoleIds.Any(c => c == x.RoleId));
                var rolesToInsert = user.OrganizationRoleIds.Where(x => !oldRoles.Any(c => c.RoleId == x));
                foreach (var roleId in rolesToInsert)
                {
                    Insert(new OrganizationUser()
                    {
                        OrganizationId = organizationId,
                        UserId = user.UserId,
                        RoleId = roleId
                    });
                }
                foreach (var roleId in rolesToDelete)
                {
                    Delete(roleId.ID);
                }
                SaveChanges();
            }
        }

        public bool UserIsInOrganization(int userId, int organizationId)
        {
            return this.GetQueryable().AsNoTracking().Any(c => c.UserId == userId && c.OrganizationId == organizationId);
        }

        public bool UserIsInRole(int organizationId, int userId, int roleId)
        {
            return this.GetQueryable().AsNoTracking().Any(c => c.UserId == userId && c.OrganizationId == organizationId && c.RoleId == roleId);
        }

        public async Task<bool> UserIsInOrganizationAsync(int userId, int organizationId)
        {
            return await this.GetQueryable().AsNoTracking().AnyAsync(c => c.UserId == userId && c.OrganizationId == organizationId);
        }
    }
}
