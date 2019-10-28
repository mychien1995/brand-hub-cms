using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Models.Authentication
{
    public class SignInResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public UserModel User { get; set; }
    }
}
