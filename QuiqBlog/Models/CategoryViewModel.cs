using Microsoft.AspNetCore.Mvc.Rendering;
using QuiqBlog.Data.Models;
using System.Collections.Generic;

namespace QuiqBlog.Models
{
    public class CategoryViewModel
    {
        public List<Post> Posts { get; set; }
        public SelectList Categories { get; set; }
        public string PostCategory { get; set; }
        public string SearchString { get; set; }
    }
}
