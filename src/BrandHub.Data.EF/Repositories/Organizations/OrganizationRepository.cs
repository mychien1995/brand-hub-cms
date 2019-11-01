using BrandHub.Data.EF.Databases;
using BrandHub.Data.EF.Entities;
using BrandHub.Framework.IoC;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Data.EF.Repositories.Organizations
{
    public interface IOrganizationRepository : IEntityRepository<Organization>
    {

    }

    [ServiceTypeOf(typeof(IOrganizationRepository))]
    public class OrganizationRepository : BaseRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(BrandHubDbContext context) : base(context)
        {

        }
    }
}
