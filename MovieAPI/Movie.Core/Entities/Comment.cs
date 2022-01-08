using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Core.Entities
{
    public class Comment
    {
        public string Key { get; set; }
        public string Uid { get; set; }
        public string Message { get; set; }
        public DateTime When { get; set; } = DateTime.Now;
        public string ReplyComment { get; set; } = "";
        public string UserLiked { get; set; } = "";
        public int Likes { get; set; } = 0;
    }
}
