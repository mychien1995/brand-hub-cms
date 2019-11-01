using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Models.Organizations
{
    public class HostDefinitionModel
    {
        public int ID { get; set; }
        public int OrganizationId { get; set; }
        public string HostName { get; set; }
    }
}
