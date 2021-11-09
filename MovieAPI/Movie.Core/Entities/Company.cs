using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core.Entities
{
    public class Company
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [JsonProperty("name")]
        public string Name { get; set; }
        public string Logo { get; set; }
        [JsonProperty("home_page")]
        public string HomePage { get; set; }
        [JsonProperty("head_quarter")]
        public string HeadQuarter { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
