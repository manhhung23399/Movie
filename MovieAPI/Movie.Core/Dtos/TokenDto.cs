using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Core.Dtos
{
    public class TokenDto
    {
        public string id_token { get; set; }
        public string refresh_token { get; set; }
        public string expires_in { get; set; }
    }
}
