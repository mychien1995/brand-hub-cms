using BrandHub.CMS.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrandHub.CMS.Api.Attributes
{
    public class FrameworkExceptionHandlerAttribute : TypeFilterAttribute
    {
        public FrameworkExceptionHandlerAttribute() : base(typeof(FrameworkExceptionFilter))
        {
        }
    }
}
