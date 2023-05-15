using System;
using System.Collections.Generic;
using System.Text;

namespace QuiqBlog.Data.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string SubscriberId { get; set; }
        public string AuthorId { get; set; }
    }
}
