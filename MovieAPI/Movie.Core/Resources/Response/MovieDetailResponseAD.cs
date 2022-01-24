using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Core.Resources.Response
{
    public class MovieDetailResponseAD
    {
        public string Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Poster { get; set; }
        public string? BackDrop { get; set; }
        public List<string> Genres { get; set; }
        public List<string> Cats { get; set; }
        public List<string> Companies { get; set; }
        public DateTime DateRelease { get; set; }
        public string Source { get; set; }
    }
}
