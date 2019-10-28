using BrandHub.Constants;
using BrandHub.Data.EF.Databases;
using BrandHub.Framework.Initializations;
using BrandHub.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrandHub.Data.EF
{
    public class DatabaseInitializationModule : IInitializableModule
    {
        public void Initialize(IServiceCollection services)
        {
            var appConfiguration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.AddDbContext<BrandHubDbContext>(x => x.UseSqlServer(appConfiguration.GetConnectionString("Default")));
            var dbContext = services.BuildServiceProvider().GetService<BrandHubDbContext>();
            var authenticationSection = appConfiguration.GetSection(AuthenticationConstants.AuthenticationSection);
            var adminUsername = authenticationSection?.GetValue(AuthenticationConstants.AdminUsername, "admin") ?? "admin";
            var adminPassword = authenticationSection?.GetValue(AuthenticationConstants.AdminPassword, "123456aA@") ?? "123456aA@";
            var adminPasswordSalt = Guid.NewGuid().ToString("N");
            Entities.ApplicationUser admin = dbContext.Users.FirstOrDefault(x => x.Username == adminUsername);
            if (admin == null)
            {
                admin = new Entities.ApplicationUser()
                {
                    CreatedDate = DateTime.Now,
                    Email = "admin@brandhub.com",
                    Fullname = "System Admin",
                    IsActive = true,
                    IsDeleted = false,
                    IsEmailConfirmed = true,
                    UpdatedDate = DateTime.Now,
                    Username = adminUsername,
                    PasswordSalt = adminPasswordSalt,
                    PasswordHash = EncryptUtils.SHA256Encrypt(adminPassword, adminPasswordSalt)
                };
                dbContext.Users.Add(admin);
            }
            var hasAdminRole = dbContext.Roles.Any(x => x.ID == (int)SystemRoles.OrganizationAdmin);
            if (!hasAdminRole)
            {
                dbContext.Roles.Add(new Entities.ApplicationRole()
                {
                    ID = (int)SystemRoles.SystemAdmin,
                    RoleName = "System Admin"
                });
                dbContext.Roles.Add(new Entities.ApplicationRole()
                {
                    ID = (int)SystemRoles.Developer,
                    RoleName = "Developer"
                });
                dbContext.Roles.Add(new Entities.ApplicationRole()
                {
                    ID = (int)SystemRoles.OrganizationAdmin,
                    RoleName = "Organization Admin"
                });
                dbContext.Roles.Add(new Entities.ApplicationRole()
                {
                    ID = (int)SystemRoles.OrganizationUser,
                    RoleName = "Organization User"
                });
            }
            if (admin == null) return;
            var adminInAdminRole = dbContext.UserRoles.Any(x => x.RoleId == (int)SystemRoles.SystemAdmin && x.UserId == admin.ID);
            if (!adminInAdminRole)
            {
                dbContext.UserRoles.Add(new Entities.ApplicationUserRole()
                {
                    RoleId = (int)SystemRoles.SystemAdmin,
                    UserId = admin.ID
                });
            }
            dbContext.SaveChanges();
        }
    }
}
