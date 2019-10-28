using BrandHub.CMS.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrandHub.CMS.Api.Attributes
{
    public class FrameworkAuthorizedAttribute : TypeFilterAttribute
    {
        public FrameworkAuthorizedAttribute() : base(typeof(FrameworkAuthorizedFilter))
        {
        }
    }
}
