using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Core.Resources.Response
{
    public class CastDetailResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Biography { get; set; }
        public string Country { get; set; }
        public List<MovieResponse> Movies { get; set; } = new List<MovieResponse>();
    }
}
