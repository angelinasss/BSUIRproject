using QuiqBlog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuiqBlog.Data.Models {
    public class Post {
        public int Id { get; set; }
        public ApplicationUser Creator { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool Published { get; set; }

        public bool Approved { get; set; }
        public ApplicationUser Approver { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }

        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        [DisplayName("Категория")]
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        [DisplayName("Тип поста")]
        public PostType PostType { get; set; }
        public int PostTypeId { get; set; }

    }
}