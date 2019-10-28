using BrandHub.Constants;
using BrandHub.Data.EF.Databases;
using BrandHub.Data.EF.Entities;
using BrandHub.Framework.Configurations;
using BrandHub.Framework.IoC;
using BrandHub.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BrandHub.Data.EF.Repositories.Users
{
    public interface IUserTokenRepository : IEntityRepository<ApplicationUserToken>
    {
        string GetToken(int userId, string userName);
    }

    [ServiceTypeOf(typeof(IUserTokenRepository))]
    public class UserTokenRepository : BaseRepository<ApplicationUserToken>, IUserTokenRepository
    {
        private IAppSettingReader _appSettingReader;
        private int TokenDurationInMinutes = 30;
        public UserTokenRepository(BrandHubDbContext context, IAppSettingReader appSettingReader) : base(context)
        {
            _appSettingReader = appSettingReader;
            TokenDurationInMinutes = _appSettingReader.GetValue<int>(AuthenticationConstants.TokenDurationKey, 30);
        }

        public string GetToken(int userId, string userName)
        {
            var now = DateTime.UtcNow;
            var existingToken = this.GetQueryable().AsNoTracking().FirstOrDefault(x => x.UserId == userId);
            if (existingToken != null)
            {
                if (existingToken.ExpiredTime > now)
                    return existingToken.Value;
                this.Delete(existingToken.ID);
            }
            var randomToken = EncryptUtils.GenerateAccessToken();
            //var encryptedToken = EncryptUtils.SHA256Encrypt(randomToken, userName);
            var token = new ApplicationUserToken()
            {
                StartedTime = now,
                UserId = userId,
                Value = randomToken,
                ExpiredTime = now.AddMinutes(TokenDurationInMinutes)
            };
            this.Insert(token);
            this.SaveChanges();
            return randomToken;
        }
    }
}
