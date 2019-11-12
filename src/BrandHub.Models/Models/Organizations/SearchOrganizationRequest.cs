using BrandHub.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BrandHub.Models.Organizations
{
    public class SearchOrganizationRequest : IPagingRequest
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public string OrderBy { get; set; }
    }
}
