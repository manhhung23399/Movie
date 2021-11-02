using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Models.Entities
{
    public class Cast
    {
        public int id { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
        public string biography { get; set; }
        public string place { get; set; }
        public DateTime birthday { get; set; }
        public DateTime deadDay { get; set; }
    }
}
