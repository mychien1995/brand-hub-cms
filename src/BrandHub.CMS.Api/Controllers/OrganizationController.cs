using BrandHub.CMS.Api.Attributes;
using BrandHub.Models;
using BrandHub.Models.Organizations;
using BrandHub.Models.Users;
using BrandHub.Services.Organizations;
using BrandHub.Services.Users;
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

    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }
        [HttpPost]
        public ActionResult<OperationResult<int?>> Create([FromBody]CreateOrganizationRequest model)
        {
            var result = _organizationService.CreateOrganization(model);
            return new OperationResult<int?>()
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data?.ID,
                Message = result.Message
            };
        }
    }
}
