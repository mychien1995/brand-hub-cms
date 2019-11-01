using BrandHub.Models.Adresses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Models.Organizations
{
    public class OrganizationModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AddressId { get; set; }
        public virtual AddressModel Address { get; set; }
        public virtual List<HostDefinitionModel> WebsiteHosts { get; set; }
    }
}
