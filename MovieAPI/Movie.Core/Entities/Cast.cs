using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core.Entities
{
    public class Cast
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        public string FileName { get; set; }
        [JsonProperty("biography")]
        public string Biography { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
