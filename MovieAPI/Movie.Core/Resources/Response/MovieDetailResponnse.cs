using System;
using System.Collections.Generic;
using System.Text;
using Movie.Core.Entities;
using Movie.Core.Utils;
using Newtonsoft.Json;

namespace Movie.Core.Resources.Response
{
    public class MovieDetailResponnse
    {
        [JsonProperty("title")]
        public string? Title { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
        [JsonProperty("poster")]
        public string? Poster { get; set; }
        [JsonProperty("vote")]
        public double Vote { get; set; } = RandomNumber.GetRandomNumber(0.0, 10.0);
        [JsonProperty("vote_count")]
        public int VoteCount { get; set; } = RandomNumber.GetRandomNumber(500, 4000);
        [JsonProperty("casts")]
        public List<Cast>? Casts { get; set; } = new List<Cast>();
        [JsonProperty("genres")]
        public List<Genre>? Genres { get; set; } = new List<Genre>();
        [JsonProperty("companies")]
        public List<Company>? Companies { get; set; } = new List<Company>();

    }
}
