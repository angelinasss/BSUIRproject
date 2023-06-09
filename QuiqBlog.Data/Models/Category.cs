﻿using QuiqBlog.Data.Models;
using System.Collections.Generic;

namespace QuiqBlog.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<Post> Posts { get; set; }
    }
}
