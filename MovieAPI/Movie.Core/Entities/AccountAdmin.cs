using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core.Entities
{
    public class AccountAdmin : EntityBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
