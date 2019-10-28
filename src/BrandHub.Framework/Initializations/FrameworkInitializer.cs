using BrandHub.Framework.IoC;
using BrandHub.Utilities.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BrandHub.Framework.Initializations
{
    public class FrameworkInitializer
    {
        public const string APPLICATION_CONFIG_PATH = "/Config/application.json";
        public static void InitializeServices(IServiceCollection services)
        {
            var assemblies = AssemblyHelper.GetAssemblies(x => x.Name.StartsWith("BrandHub")).ToArray();
            InitializeServices(services, assemblies);

        }
        public static void InitializeServices(IServiceCollection services, Assembly[] assemblies)
        {
            var types = AssemblyHelper.GetClassesWithAttribute(assemblies, typeof(ServiceTypeOfAttribute));
            foreach (var type in types)
            {
                var attr = ((ServiceTypeOfAttribute)type.GetCustomAttribute(typeof(ServiceTypeOfAttribute)));
                var parentType = attr.ServiceType;
                var scope = attr.LifetimeScope;
                switch (scope)
                {
                    case LifetimeScope.Transient:
                        services.AddTransient(parentType, type);
                        break;
                    case LifetimeScope.Singleton:
                        services.AddSingleton(parentType, type);
                        break;
                    case LifetimeScope.PerRequest:
                        services.AddScoped(parentType, type);
                        break;
                    default:
                        services.AddTransient(parentType, type);
                        break;
                }
            }
        }
        public static void InitializeModules(IServiceCollection services)
        {
            var hostingEnvironment = services.BuildServiceProvider().GetService<IHostingEnvironment>();
            string webRootPath = hostingEnvironment.ContentRootPath;
            var configFilePath = webRootPath + APPLICATION_CONFIG_PATH;
            var configFileContent = System.IO.File.ReadAllText(configFilePath);
            var configModule = JsonConvert.DeserializeObject<ApplicationConfig>(configFileContent);
            var initializationModules = configModule.InitializationModules;
            if (initializationModules != null)
            {
                foreach (var moduleType in initializationModules)
                {
                    var instanceType = Type.GetType(moduleType.Type, true, true);
                    var instance = Activator.CreateInstance(instanceType);
                    if (instance is IInitializableModule)
                    {
                        ((IInitializableModule)instance).Initialize(services);
                    }
                }
            }
        }
    }
}
