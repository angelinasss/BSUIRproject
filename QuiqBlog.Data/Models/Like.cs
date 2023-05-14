using System;
using System.Collections.Generic;
using System.Text;

namespace QuiqBlog.Data.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public bool IsLiked { get; set; }
        public bool IsDisliked { get; set; }
    }
}