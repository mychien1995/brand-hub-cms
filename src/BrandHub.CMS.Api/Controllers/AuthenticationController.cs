using BrandHub.CMS.Api.Models;
using BrandHub.Models;
using BrandHub.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrandHub.CMS.Api.Controllers
{
    [Route("cms/api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IAccessTokenService _accessTokenService;
        public AuthenticationController(IUserManager userManager, IAccessTokenService accessTokenService)
        {
            _userManager = userManager;
            _accessTokenService = accessTokenService;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<ActionResult> Authenticate([FromBody]UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _userManager.PasswordSignInAsync(model.Username, model.Password, model.Hostname);
                if (!loginResult.Success)
                {
                    return BadRequest(loginResult.Message);
                }
                var user = loginResult.User;
                var token = _accessTokenService.GetToken(user.ID, user.Username);
                return Ok(token);

            }
            return BadRequest(ModelState);
        }
    }
}
