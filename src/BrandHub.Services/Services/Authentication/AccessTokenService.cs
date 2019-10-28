using BrandHub.Data.EF.Repositories.Users;
using BrandHub.Framework.IoC;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Services.Authentication
{
    public interface IAccessTokenService
    {
        string GetToken(int userId, string userName);
        bool IsValidToken(string token, string username);
    }

    [ServiceTypeOf(typeof(IAccessTokenService))]
    public class AccessTokenService : IAccessTokenService
    {
        private readonly IUserTokenRepository _userTokenRepository;
        public AccessTokenService(IUserTokenRepository userTokenRepository)
        {
            _userTokenRepository = userTokenRepository;
        }
        public string GetToken(int userId, string userName)
        {
            return _userTokenRepository.GetToken(userId, userName);
        }

        public bool IsValidToken(string token, string username)
        {
            return _userTokenRepository.IsValidToken(token, username);
        }
    }
}
