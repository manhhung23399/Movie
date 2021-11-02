using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Models.Entities
{
    public class Company
    {
        public int id { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
        public string homePage { get; set; }
        public string headQuarter { get; set; }
        public string country { get; set; }
        public List<Movie> movies {get;set;}
    }
}
