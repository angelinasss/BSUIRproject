using System;
using System.Collections.Generic;
using System.Text;

namespace QuiqBlog.Data.Models
{
    public class PostType
    {
        public int Id { get; set; }
        public string PostTypeTitle { get; set; }
        public IList<Post> Posts { get; set; }
    }
}
