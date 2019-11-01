using BrandHub.Data.EF.Entities;
using BrandHub.Models;
using BrandHub.Models.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Data.EF.Extensions
{
    public static class Mapper
    {
        #region ApplicationUser
        public static UserModel ToModel(this ApplicationUser entity)
        {
            if (entity == null) return null;
            return new UserModel()
            {
                ID = entity.ID,
                Username = entity.Username,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                PasswordHash = entity.PasswordHash,
                PasswordSalt = entity.PasswordSalt,
                Fullname = entity.Fullname,
                IsActive = entity.IsActive,
                IsEmailConfirmed = entity.IsEmailConfirmed,
                IsDeleted = entity.IsDeleted,
                LastLoginDate = entity.LastLoginDate,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate
            };
        }

        public static ApplicationUser ToEntity(this UserModel model)
        {
            if (model == null) return null;
            return new ApplicationUser()
            {
                ID = model.ID,
                Username = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                PasswordHash = model.PasswordHash,
                PasswordSalt = model.PasswordSalt,
                Fullname = model.Fullname,
                IsActive = model.IsActive,
                IsEmailConfirmed = model.IsEmailConfirmed,
                IsDeleted = model.IsDeleted,
                LastLoginDate = model.LastLoginDate,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate
            };
        }
        #endregion

        #region Product
        public static OrganizationModel ToModel(this Organization entity)
        {
            if (entity == null) return null;
            return new OrganizationModel()
            {
                ID = entity.ID,
                Name = entity.Name,
                IsActive = entity.IsActive,
                IsDeleted = entity.IsDeleted,
                CreatedDate = entity.CreatedDate,
                AddressId = entity.AddressId,
            };
        }

        public static Organization ToEntity(this OrganizationModel model)
        {
            if (model == null) return null;
            return new Organization()
            {
                ID = model.ID,
                Name = model.Name,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                CreatedDate = model.CreatedDate,
                AddressId = model.AddressId
            };
        }
        #endregion
    }
}
