﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Models.Users
{
    public class CreateUserModel : UserModel
    {
        public CreateUserModel()
        {
            OrganizationRoleIds = new List<int>();
        }
        public int CreateUserRoleId { get; set; }
        public int? OrganizationId { get; set; }
        public List<int> OrganizationRoleIds { get; set; }
        public int RoleId { get; set; }
    }
}
