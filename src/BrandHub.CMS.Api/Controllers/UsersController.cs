using BrandHub.CMS.Api.Attributes;
using BrandHub.Models;
using BrandHub.Models.Users;
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

    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public ActionResult<OperationResult<int?>> Create([FromBody]CreateUserModel model)
        {
            var result = _userService.CreateUser(model);
            return new OperationResult<int?>()
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data?.ID,
                Message = result.Message
            };
        }
    }
}
