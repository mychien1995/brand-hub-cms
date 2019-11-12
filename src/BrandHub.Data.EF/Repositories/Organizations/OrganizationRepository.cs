using BrandHub.Data.EF.Databases;
using BrandHub.Data.EF.Entities;
using BrandHub.Framework.IoC;
using BrandHub.Models.Organizations;
using BrandHub.Models.Shared;
using BrandHub.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace BrandHub.Data.EF.Repositories.Organizations
{
    public interface IOrganizationRepository : IEntityRepository<Organization>
    {
        SearchResult<Organization> Search(SearchOrganizationRequest request);
    }

    [ServiceTypeOf(typeof(IOrganizationRepository))]
    public class OrganizationRepository : BaseRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(BrandHubDbContext context) : base(context)
        {

        }

        public SearchResult<Organization> Search(SearchOrganizationRequest request)
        {
            var result = new SearchResult<Organization>();
            var queryable = BuildQuery(request);
            if (request.PageIndex == 0)
            {
                result.Total = queryable.Count();
            }
            if (request.OrderBy == null) queryable = queryable.OrderBy(x => x.CreatedDate);
            else queryable = queryable.OrderByProperty(request.OrderBy);
            if (request.PageIndex != null && request.PageSize != null)
            {
                queryable = queryable.Skip(request.PageSize.Value * request.PageIndex.Value).Take(request.PageSize.Value);
            }
            result.Result = queryable.ToList();
            return result;
        }

        private IQueryable<Organization> BuildQuery(SearchOrganizationRequest request)
        {
            var queryable = this.GetQueryable().Include(x => x.Address).AsNoTracking();
            if (request.ID != null)
            {
                queryable = queryable.Where(x => x.ID == request.ID);
            }
            if (!string.IsNullOrEmpty(request.Name))
            {
                queryable = queryable.Where(x => x.Name.Contains(request.Name));
            }
            if (request.IsActive != null)
            {
                queryable = queryable.Where(x => x.IsActive == request.IsActive);
            }
            if (request.IsDeleted != null)
            {
                queryable = queryable.Where(x => x.IsDeleted == request.IsDeleted);
            }
            if (request.CountryId != null)
            {
                queryable = queryable.Where(x => x.Address.CountryId == request.CountryId);
            }
            if (request.ProvinceId != null)
            {
                queryable = queryable.Where(x => x.Address.ProvinceId == request.ProvinceId);
            }
            return queryable;
        }
    }
}
