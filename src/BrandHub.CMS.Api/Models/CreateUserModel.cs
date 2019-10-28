using BrandHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrandHub.CMS.Api.Models
{
    public class CreateUserModel : UserModel
    {
        public int? OrganizationId { get; set; }
        public int Role { get; set; }
    }
}
