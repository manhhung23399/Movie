using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core.Entities
{
    public class Cast : EntityBase
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string FileName { get; set; }
        public string Biography { get; set; }
    }
}
