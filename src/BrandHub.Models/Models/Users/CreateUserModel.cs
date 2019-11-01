using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Models.Users
{
    public class CreateUserModel : UserModel
    {
        public CreateUserModel()
        {
            RoleIds = new List<int>();
        }
        public List<int> RoleIds { get; set; }
        public int? OrganizationId { get; set; }
        public int? OrganizationRoleId { get; set; }
    }
}
