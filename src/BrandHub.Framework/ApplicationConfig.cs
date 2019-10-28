using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Framework
{
    public class ApplicationConfig
    {
        [JsonProperty("initializations")]
        public JsonClassLibrary[] InitializationModules { get; set; }
    }

    public class JsonClassLibrary
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
