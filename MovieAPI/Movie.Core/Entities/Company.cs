using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core.Entities
{
    public class Company : EntityBase
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public string HomePage { get; set; }
        public string HeadQuarter { get; set; }
        public string Country { get; set; }
    }
}
