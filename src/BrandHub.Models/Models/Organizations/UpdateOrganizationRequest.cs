using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrandHub.Models.Organizations
{
    public class UpdateOrganizationRequest : OrganizationModel
    {
        public string[] HostNames { get; set; }
        public string AddressLine { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int? DistrictId { get; set; }

        [JsonIgnore]
        public bool IncludeHostname
        {
            get
            {
                return HostNames != null && HostNames.Any();
            }
        }
    }
}
