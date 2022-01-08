using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Core.Utils
{
    public static class PropertyExchange
    {
        public static dynamic ReClass(this object dumpObject)
        {
            var serilaizeJson = JsonConvert.SerializeObject(dumpObject, Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                });

            var result = JsonConvert.DeserializeObject<dynamic>(serilaizeJson);
            return result;
        }
    }
}
