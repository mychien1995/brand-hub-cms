using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Interfaces
{
    public interface IChangeTrackable
    {
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
