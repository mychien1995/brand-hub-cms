using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Services
{
    public class Constants
    {
        public class Messages
        {
            public const string INVALID_LOGIN = "Username or password incorrect";
            public const string USER_INACTIVE = "This account is inactive";
            public const string INVALID_HOSTNAME = "This account does not belong to this domain";
            public const string USER_REQUIRE_ORGANIZATION = "Organization is required for this user";
            public const string USER_UNIQUE_EMAIL = "An user with this email is already existed";
            public const string USER_UNIQUE_PHONE = "An user with this phone number is already existed";
            public const string USER_UNIQUE_USERNAME = "An user with this username is already existed";
        }
    }
}
