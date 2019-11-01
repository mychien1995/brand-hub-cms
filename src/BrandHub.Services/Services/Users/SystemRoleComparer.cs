using BrandHub.Constants;
using BrandHub.Framework.IoC;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Services.Users
{
    public interface ISystemRoleComparer
    {
        bool CanMadeChange(int igniteRole, int targetRole);
    }

    [ServiceTypeOf(typeof(ISystemRoleComparer))]
    public class SystemRoleComparer : ISystemRoleComparer
    {
        public bool CanMadeChange(int igniteRole, int targetRole)
        {
            if (igniteRole == targetRole && igniteRole == (int)SystemRoles.Developer) return true;
            if (targetRole == (int)SystemRoles.Developer) return false;
            if (targetRole == (int)SystemRoles.SystemAdmin && igniteRole != (int)SystemRoles.Developer) return false;
            if (targetRole == (int)SystemRoles.OrganizationAdmin && igniteRole != (int)SystemRoles.SystemAdmin && igniteRole != (int)SystemRoles.Developer) return false;
            if (targetRole == (int)SystemRoles.OrganizationUser && igniteRole == targetRole) return false;
            return true;

        }
    }
}
