using BrandHub.Data.EF.Databases;
using BrandHub.Data.EF.Entities;
using BrandHub.Framework.IoC;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandHub.Data.EF.Repositories.Organizations
{
    public interface IHostDefinitionRepository : IEntityRepository<HostDefinition>
    {
        HostDefinition FindByName(string hostName);
        Task<HostDefinition> FindByNameAsync(string hostName);
        List<HostDefinition> GetByOrganization(int organizationId);
    }

    [ServiceTypeOf(typeof(IHostDefinitionRepository))]
    public class HostDefinitionRepository : BaseRepository<HostDefinition>, IHostDefinitionRepository
    {
        public HostDefinitionRepository(BrandHubDbContext context) : base(context)
        {

        }

        public List<HostDefinition> GetByOrganization(int organizationId)
        {
            return this.GetQueryable().AsNoTracking().Where(x => x.OrganizationId == organizationId).ToList();
        }

        public HostDefinition FindByName(string hostName)
        {
            return this.GetQueryable().AsNoTracking().FirstOrDefault(x => x.HostName.ToLower() == hostName.ToLower());
        }

        public async Task<HostDefinition> FindByNameAsync(string hostName)
        {
            return await this.GetQueryable().AsNoTracking().FirstOrDefaultAsync(x => x.HostName.ToLower() == hostName.ToLower());
        }
    }
}
