using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Models.Entities
{
    public class Movie
    {
        public int id { get; set; }
        public string title { get; set; }
        public string poster { get; set; }
        public string backDrop { get; set; }
        public int score { get; set; }
        public DateTime releaseDate { get; set; }
        public Genre genres { get; set; }
        public Cast casts { get; set; }
        public Company companies { get; set; }

    }
}
