using BrandHub.Data.EF.Extensions;
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
using System.Threading.Tasks;

namespace BrandHub.Services.Authentication
{
    public interface IUserManager
    {
        Task<SignInResult> PasswordSignInAsync(string username, string password, string hostName);
    }
    [ServiceTypeOf(typeof(IUserManager))]
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IHostDefinitionRepository _hostDefinitionRepository;
        private readonly IOrganizationUserRepository _organizationUserRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        public UserManager(IUserRepository userRepository, IHostDefinitionRepository hostDefinitionRepository,
            IOrganizationUserRepository organizationUserRepository, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _hostDefinitionRepository = hostDefinitionRepository;
            _organizationUserRepository = organizationUserRepository;
            _userRoleRepository = userRoleRepository;
        }
        public async Task<SignInResult> PasswordSignInAsync(string username, string password, string hostName)
        {
            var signInResult = await PasswordSignInAsync(username, password);
            if (signInResult.Success)
            {
                var user = signInResult.User;
                _userRoleRepository.FetchRoles(user);
                if (user.Roles.Any(x => x.CanBypassDomain())) return signInResult;
                if (string.IsNullOrEmpty(hostName))
                {
                    signInResult.Success = false;
                    signInResult.Message = Constants.Messages.INVALID_HOSTNAME;
                    return signInResult;
                }
                var host = await _hostDefinitionRepository.FindByNameAsync(hostName);
                if (host == null || host.Organization == null)
                {
                    signInResult.Success = false;
                    signInResult.Message = Constants.Messages.INVALID_HOSTNAME;
                    return signInResult;
                }
                var userBelongToOrg = await _organizationUserRepository.UserIsInOrganizationAsync(user.ID, host.Organization.ID);
                if (!userBelongToOrg)
                {
                    signInResult.Success = false;
                    signInResult.Message = Constants.Messages.INVALID_HOSTNAME;
                    return signInResult;
                }
            }
            return signInResult;
        }

        public async Task<SignInResult> PasswordSignInAsync(string username, string password)
        {
            var result = new SignInResult();
            var userEntity = await _userRepository.FindByUsernameAsync(username);
            if (userEntity == null)
            {
                result.Message = Constants.Messages.INVALID_LOGIN;
                return result;
            }
            if (!userEntity.IsActive)
            {
                result.Message = Constants.Messages.USER_INACTIVE;
                return result;
            }
            var salt = userEntity.PasswordSalt;
            var passwordHash = EncryptUtils.SHA256Encrypt(password, salt);
            if (passwordHash != userEntity.PasswordHash)
            {
                result.Message = Constants.Messages.INVALID_LOGIN;
                return result;
            }
            result.User = userEntity.ToModel();
            result.Success = true;
            return result;
        }
    }
}
