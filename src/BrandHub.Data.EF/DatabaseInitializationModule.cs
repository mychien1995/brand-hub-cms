using BrandHub.Constants;
using BrandHub.Data.EF.Databases;
using BrandHub.Framework.Initializations;
using BrandHub.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrandHub.Data.EF
{
    public class DatabaseInitializationModule : IInitializableModule
    {
        public void Initialize(IServiceCollection services)
        {
            var appConfiguration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.AddDbContext<BrandHubDbContext>(x => x.UseSqlServer(appConfiguration.GetConnectionString("Default")));
        }
    }
}
