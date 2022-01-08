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
        public string Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Poster { get; set; }
        public double Vote { get; set; } = RandomNumber.GetRandomNumber(0.0, 10.0);
        public int Vote_count { get; set; } = RandomNumber.GetRandomNumber(500, 4000);
        public int Year_release { get; set; }
        public string Status { get; set; }
        public HashSet<SourceVms> Sources { get; set; } = new HashSet<SourceVms>();
        public IEnumerable<CastResponse>? Casts { get; set; } = new List<CastResponse>();
        public IEnumerable<GenresResponse>? Genres { get; set; } = new List<GenresResponse>();
        public IEnumerable<CompanyResponse>? Companies { get; set; } = new List<CompanyResponse>();
        public MovieDetailResponnse(
            string id, 
            string title, 
            string description, 
            string poster, 
            int yearRelease, 
            bool isRelease,
            double vote,
            int voteCount)
        {
            Id = id;
            Title = title;
            Description = description;
            Poster = poster;
            Year_release = yearRelease;
            Vote = vote;
            Vote_count = voteCount;
            Status = isRelease ? "Đã công chiếu" : "Chưa công chiếu";
        }
    }
    public class SourceVms
    {
        public string Label { get; set; }
        public string File { get; set; }
        public string Type { get; set; }
        public SourceVms(string label = "", string file = "", string type = "")
        {
            Label = label;
            File = file;
            Type = type;
        }
    }
}
