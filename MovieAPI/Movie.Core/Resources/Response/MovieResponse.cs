using Movie.Core.Entities;
using Movie.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Core.Resources.Response
{
    public class MovieResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string? Title { get; set; }
        [JsonProperty("poster")]
        public string? Poster { get; set; }
        [JsonProperty("backdrop")]
        public string? Backdrop { get; set; }
    }
}
