using BrandHub.Data.EF.Databases;
using BrandHub.Data.EF.Entities;
using BrandHub.Framework.IoC;
using BrandHub.Models;
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
        bool UserBelongToOrganization(int userId, int organizationId);
        Task<bool> UserBelongToOrganizationAsync(int userId, int organizationId);
    }

    [ServiceTypeOf(typeof(IOrganizationUserRepository))]
    public class OrganizationUserRepository : BaseRepository<OrganizationUser>, IOrganizationUserRepository
    {
        public OrganizationUserRepository(BrandHubDbContext context) : base(context)
        {

        }

        public bool UserBelongToOrganization(int userId, int organizationId)
        {
            return this.GetQueryable().AsNoTracking().Any(c => c.UserId == userId && c.OrganizationId == organizationId);
        }

        public async Task<bool> UserBelongToOrganizationAsync(int userId, int organizationId)
        {
            return await this.GetQueryable().AsNoTracking().AnyAsync(c => c.UserId == userId && c.OrganizationId == organizationId);
        }
    }
}
