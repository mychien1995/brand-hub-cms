using BrandHub.Framework.IoC;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Framework.Configurations
{
    public interface IAppSettingReader
    {
        T GetValue<T>(string key, T defaultValue);
    }
    [ServiceTypeOf(typeof(IAppSettingReader))]
    public class AppSettingReader : IAppSettingReader
    {
        private IConfiguration _configuration;
        public AppSettingReader(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public T GetValue<T>(string key, T defaultValue)
        {
            var keySegments = key.Split("/");
            IConfigurationSection current = null;
            for (int i = 0; i < keySegments.Length; i++)
            {
                var segment = keySegments[i];
                if (i != keySegments.Length - 1)
                {
                    var section = current == null ? _configuration.GetSection(segment) : current.GetSection(segment);
                    if (section == null) return defaultValue;
                    current = section;
                }
                var value = current == null ? _configuration.GetValue<T>(segment, defaultValue) : current.GetValue<T>(segment, defaultValue);
                return value;
            }
            return defaultValue;
        }
    }
}
