using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Models.Shared
{
    public interface IPagingRequest
    {
        int? PageSize { get; set; }
        int? PageIndex { get; set; }
    }
}
