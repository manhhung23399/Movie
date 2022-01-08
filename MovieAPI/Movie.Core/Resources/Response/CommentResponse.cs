using Movie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Core.Resources.Response
{
    public class CommentResponse
    {
        public string key { get; set; }
        public string message { get; set; }
        public DateTime when { get; set; } 
        public string reply_comment { get; set; }
        public int likes { get; set; } 
        public string user_name { get; set; }
        public string photo_url { get; set; }
    }
}
