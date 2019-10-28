using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Framework.Initializations
{
    public interface IInitializableModule
    {
        void Initialize(IServiceCollection services);
    }
}
