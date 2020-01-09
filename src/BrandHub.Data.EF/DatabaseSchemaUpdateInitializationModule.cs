using BrandHub.Data.EF.Databases;
using BrandHub.Framework.Initializations;
using DbUp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BrandHub.Data.EF
{
    public class DatabaseSchemaUpdateInitializationModule : IInitializableModule
    {
        public void Initialize(IServiceCollection services)
        {
            var appConfiguration = services.BuildServiceProvider().GetService<IConfiguration>();
            var connString = appConfiguration.GetConnectionString("Default");
            var upgrader = DeployChanges.To
                   .SqlDatabase(connString)
                   .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                   .LogToConsole()
                   .WithTransactionPerScript()
                   .Build();
            var result = upgrader.PerformUpgrade();
        }
    }
}
