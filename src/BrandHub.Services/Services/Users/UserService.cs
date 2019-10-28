using BrandHub.Data.EF.Extensions;
using BrandHub.Data.EF.Repositories.Organizations;
using BrandHub.Data.EF.Repositories.Users;
using BrandHub.Framework.IoC;
using BrandHub.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Services.Users
{
    public interface IUserService
    {
        UserModel GetUserWithRoles(int userId);
        bool UserCanAccessHost(string hostName, int userId);
    }

    [ServiceTypeOf(typeof(IUserService))]
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IHostDefinitionRepository _hostDefinitionRepository;
        private readonly IOrganizationUserRepository _organizationUserRepository;
        public UserService(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IHostDefinitionRepository hostDefinitionRepository,
            IOrganizationUserRepository organizationUserRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _hostDefinitionRepository = hostDefinitionRepository;
            _organizationUserRepository = organizationUserRepository;
        }
        public UserModel GetUserWithRoles(int userId)
        {
            var user = _userRepository.GetById(userId);
            var model = user.ToModel();
            _userRoleRepository.FetchRoles(model);
            return model;
        }

        public bool UserCanAccessHost(string hostName, int userId)
        {
            var host = _hostDefinitionRepository.FindByName(hostName);
            if (host == null) return false;
            return _organizationUserRepository.UserBelongToOrganization(userId, host.OrganizationId);
        }
    }
}
