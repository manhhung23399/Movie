using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Core.Resources.Request
{
    public class PasswordChangeRequest
    {
        public string Token { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
