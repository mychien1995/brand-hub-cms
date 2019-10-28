using BrandHub.Constants;
using BrandHub.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Models.Users
{
    public class RoleModel : IIdentity
    {
        public int ID { get; set; }
        public string RoleName { get; set; }

        public bool CanBypassDomain()
        {
            return this.ID == (int)SystemRoles.SystemAdmin || this.ID == (int)SystemRoles.Developer;
        }
    }
}
