using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Models.Organizations
{
    public class CreateOrganizationRequest : OrganizationModel
    {
        public string HostName { get; set; }
        public string AddressLine { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int? DistrictId { get; set; }
    }
}
