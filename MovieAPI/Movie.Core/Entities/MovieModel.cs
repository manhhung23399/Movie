using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core.Entities
{
    public class MovieModel : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? PosterName { get; set; } = "";
        public string? Poster { get; set; }
        public string? BackDrop { get; set; }
        public string? BackDropName { get; set; } = "";
        public double Score { get; set; }
        public DateTime DateRelease { get; set; } = DateTime.Now;
        public bool IsRelease { get; set; }
        public double Popularity { get; set; }
        public string Genres { get; set; }
        public string Casts { get; set; }
        public string Companies { get; set; }
        public Sources Sources { get; set; }
        public int VoteCount { get; set; }
        public List<Vote> Votes { get; set; } = new List<Vote>();
    }

    public class Vote
    {
        public string Uid { get; set; }
        public int VoteScore { get; set; }
    }
    public class Sources
    {
        public string mPhimMoi { get; set; }
    }
}
