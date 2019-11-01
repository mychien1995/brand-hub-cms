using BrandHub.Data.EF.Extensions;
using BrandHub.Data.EF.Repositories.Organizations;
using BrandHub.Data.EF.Repositories.Users;
using BrandHub.Framework.IoC;
using BrandHub.Models;
using BrandHub.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public OperationResult<UserModel> CreateUser(CreateUserModel createUserModel)
        {
            var result = new OperationResult<UserModel>(false);
            var userCanBypassDomain = createUserModel.Roles.All(c => c.CanBypassDomain());
            if (!userCanBypassDomain && createUserModel.OrganizationId == null)
            {
                return new OperationResult<UserModel>(false, Constants.Messages.USER_REQUIRE_ORGANIZATION);
            }
            var sameEmailUser = FindByEmail(createUserModel.Email);
            if (sameEmailUser != null)
            {
                return new OperationResult<UserModel>(false, Constants.Messages.USER_UNIQUE_EMAIL);
            }
            var samePhoneUser = FindByPhonenumber(createUserModel.PhoneNumber);
            if (samePhoneUser != null)
            {
                return new OperationResult<UserModel>(false, Constants.Messages.USER_UNIQUE_PHONE);
            }
            var sameUsernameUser = FindByUsername(createUserModel.Username);
            if (sameUsernameUser != null)
            {
                return new OperationResult<UserModel>(false, Constants.Messages.USER_UNIQUE_USERNAME);
            }
            var userEntity = createUserModel.ToEntity();
            var newUser = _userRepository.Insert(userEntity);
            _userRepository.SaveChanges();
            return null;

        }

        public UserModel FindByEmail(string email)
        {
            var user = this._userRepository.GetQueryable().AsNoTracking().FirstOrDefault(c => c.Email == email);
            if (user != null) return user.ToModel();
            return null;
        }

        public UserModel FindByUsername(string username)
        {
            var user = this._userRepository.GetQueryable().AsNoTracking().FirstOrDefault(c => c.Username == username);
            if (user != null) return user.ToModel();
            return null;
        }

        public UserModel FindByPhonenumber(string phoneNumber)
        {
            var user = this._userRepository.GetQueryable().AsNoTracking().FirstOrDefault(c => c.PhoneNumber == phoneNumber);
            if (user != null) return user.ToModel();
            return null;
        }
    }
}
