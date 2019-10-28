using BrandHub.Data.EF.Repositories.Organizations;
using BrandHub.Data.EF.Repositories.Users;
using BrandHub.Framework.IoC;
using BrandHub.Models;
using BrandHub.Models.Authentication;
using BrandHub.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrandHub.Services.Authentication
{
    public interface IUserManager
    {
        SignInResult PasswordSignIn(string username, string password, string hostName, out UserModel user);
        SignInResult PasswordSignIn(string username, string password, out UserModel user);
    }
    [ServiceTypeOf(typeof(IUserManager))]
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IHostDefinitionRepository _hostDefinitionRepository;
        private readonly IOrganizationUserRepository _organizationUserRepository;
        public UserManager(IUserRepository userRepository, IHostDefinitionRepository hostDefinitionRepository, IOrganizationUserRepository organizationUserRepository)
        {
            _userRepository = userRepository;
            _hostDefinitionRepository = hostDefinitionRepository;
            _organizationUserRepository = organizationUserRepository;
        }

        public SignInResult PasswordSignIn(string username, string password, string hostName, out UserModel user)
        {
            var signInResult = PasswordSignIn(username, password, hostName, out user);
            if (signInResult.Success)
            {
                if (user.Roles.Any(x => x.CanBypassDomain())) return signInResult;
                if (string.IsNullOrEmpty(hostName))
                {
                    signInResult.Success = false;
                    signInResult.Message = Constants.Messages.INVALID_HOSTNAME;
                    return signInResult;
                }
                var host = _hostDefinitionRepository.FindByName(hostName);
                if (host == null || host.Organization == null)
                {
                    signInResult.Success = false;
                    signInResult.Message = Constants.Messages.INVALID_HOSTNAME;
                    return signInResult;
                }
                var userBelongToOrg = _organizationUserRepository.UserBelongToOrganization(user.ID, host.Organization.ID);
                if (!userBelongToOrg)
                {
                    signInResult.Success = false;
                    signInResult.Message = Constants.Messages.INVALID_HOSTNAME;
                    return signInResult;
                }
            }
            return signInResult;
        }

        public SignInResult PasswordSignIn(string username, string password, out UserModel user)
        {
            user = null;
            var result = new SignInResult();
            var userEntity = _userRepository.FindByUsername(username);
            if (userEntity == null)
            {
                result.Message = Constants.Messages.INVALID_LOGIN;
                return result;
            }
            if (userEntity.IsActive)
            {
                result.Message = Constants.Messages.USER_INACTIVE;
                return result;
            }
            var salt = userEntity.PasswordSalt;
            var passwordHash = EncryptUtils.SHA256Encrypt(password, salt);
            if (passwordHash != user.PasswordHash)
            {
                result.Message = Constants.Messages.INVALID_LOGIN;
                return result;
            }
            user = new UserModel();
            return result;
        }
    }
}
