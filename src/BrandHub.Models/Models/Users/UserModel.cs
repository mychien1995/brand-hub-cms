using BrandHub.Interfaces;
using BrandHub.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Models
{
    public class UserModel : IChangeTrackable, IIdentity, IPreservable
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Fullname { get; set; }
        public bool IsActive { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<RoleModel> Roles { get; set; }
    }
}
