﻿using BrandHub.CMS.Api.Attributes;
using BrandHub.Models;
using BrandHub.Models.Organizations;
using BrandHub.Models.Shared;
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

    public class OrganizationController : BaseController
    {
        private readonly IOrganizationService _organizationService;
        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet("{id}")]
        public ActionResult<OrganizationModel> Get(int id)
        {
            var result = _organizationService.GetOrganization(id);
            return result;
        }

        [HttpGet]
        [Route("search")]
        public ActionResult<SearchResult<OrganizationModel>> Search([FromQuery]SearchOrganizationRequest request)
        {
            var result = _organizationService.SearchOrganization(request);
            return result;
        }

        [HttpPost]
        public ActionResult<OperationResult<int?>> Create([FromBody]UpdateOrganizationRequest model)
        {
            model.IsActive = true;
            model.CreatedDate = DateTime.UtcNow;
            var result = _organizationService.CreateOrganization(model);
            return new OperationResult<int?>()
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data?.ID,
                Message = result.Message
            };
        }

        [HttpPut]
        public ActionResult<OperationResult<int?>> Update([FromBody]UpdateOrganizationRequest model)
        {
            model.IsDeleted = false;
            model.UpdatedDate = DateTime.UtcNow;
            var result = _organizationService.UpdateOrganization(model);
            return new OperationResult<int?>()
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data?.ID,
                Message = result.Message
            };
        }

        [HttpDelete]
        public ActionResult<OperationResult<bool>> Delete([FromQuery]int id)
        {
            var result = _organizationService.DeleteOrganization(id);
            return new OperationResult<bool>()
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data,
                Message = result.Message
            };
        }
    }
}
