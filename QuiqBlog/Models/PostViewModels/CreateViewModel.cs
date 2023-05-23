using Microsoft.AspNetCore.Http;
using QuiqBlog.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuiqBlog.Models.PostViewModels
{
    public class CreateViewModel
    {
        [Required, Display(Name = "Header Image")]
        public IFormFile HeaderImage { get; set; }
        public Post Post { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<PostType> PostTypes { get; set; }
    }
}