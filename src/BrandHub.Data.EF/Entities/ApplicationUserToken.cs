using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Data.EF.Entities
{
    public class ApplicationUserToken
    {
        public string Value { get; set; }
        public int UserId { get; set; }

        public DateTime StartedTime { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
