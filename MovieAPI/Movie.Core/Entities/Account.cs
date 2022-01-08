using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core.Entities
{
    public class Account : EntityBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public bool IsEmailVerified { get; set; }
        public string Role { get; set; }
        public string PhotoUrl { get; set; } = "https://i.imgur.com/1JB4vdc.png";
    }
    public class AccountRegister : Account
    {
        public string ConfirmPassowrd { get; set; }
        public string DisplayName { get; set; }
        public string Role { get; set; } = "Client";
    }
}
