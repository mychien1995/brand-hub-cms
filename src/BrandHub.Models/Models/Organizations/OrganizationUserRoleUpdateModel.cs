using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Models.Organizations
{
    public class OrganizationUserRoleUpdateModel
    {
        public int OrganizationId { get; set; }
        public List<OrganizationUserUpdateModel> Users { get; set; }
    }

    public class OrganizationUserUpdateModel
    {
        public int UserId { get; set; }
        public List<int> OrganizationRoleIds { get; set; }
    }
}
