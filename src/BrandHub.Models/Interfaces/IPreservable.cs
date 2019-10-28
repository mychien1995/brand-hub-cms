using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Interfaces
{
    public interface IPreservable
    {
        bool IsDeleted { get; set; }
    }
}
