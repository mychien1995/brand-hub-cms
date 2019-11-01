using BrandHub.Constants;
using BrandHub.Data.EF.Repositories.Users;
using BrandHub.Services.Authentication;
using BrandHub.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrandHub.CMS.Api.Filters
{
    public class FrameworkAuthorizedFilter : IAuthorizationFilter
    {
        private readonly IAccessTokenService _accessTokenService;
        private readonly IUserService _userService;
        public FrameworkAuthorizedFilter(IAccessTokenService accessTokenService, IUserService userService)
        {
            _accessTokenService = accessTokenService;
            _userService = userService;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (actionDescriptor == null) return;
            var skipAuthorization = actionDescriptor.MethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Length > 0;
            if (skipAuthorization) return;
            var authToken = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key.ToUpper() == "Authorization".ToUpper()).Value.ToString();
            var domain = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key.ToUpper() == "Domain".ToUpper()).Value.ToString();
            if (string.IsNullOrEmpty(authToken) || string.IsNullOrEmpty(domain))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            int userId;
            var tokenValid = _accessTokenService.IsValidToken(authToken, out userId);
            if (!tokenValid)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var user = _userService.GetUserById(userId, true);
            if (user == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            if (user.Roles.Any(c => c.CanBypassDomain())) return;
            if (!_userService.UserCanAccessHost(domain, userId))
            {
                context.Result = new ForbidResult();
                return;
            }
            return;
        }
    }
}
