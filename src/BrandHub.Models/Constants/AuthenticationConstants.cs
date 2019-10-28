using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Constants
{
    public class AuthenticationConstants
    {
        public const string TokenDurationKey = "authentication/tokenDuration";
        public const string AuthenticationSection = "authentication";
        public const string AdminUsername = "sysAdminUsername";
        public const string AdminPassword = "sysAdminPwd";
    }

    public class AuthenticationMessages
    {
        public const string USER_UNAUTHENTICATED = "User is unauthenticated";
        public const string USER_DOES_NOT_BELONG_TO_HOST = "User cannot access this domain";
    }
}
