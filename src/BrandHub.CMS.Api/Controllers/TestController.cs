using BrandHub.CMS.Api.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrandHub.CMS.Api.Controllers
{
    [Route("cms/api/[controller]")]
    [ApiController]
    [FrameworkAuthorized]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Authorized";
        }
    }
}
