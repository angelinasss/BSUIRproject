using QuiqBlog.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuiqBlog.Data
{
    public static class PostTypeInitialize
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.PostTypes.Any())
            {
                context.PostTypes.AddRange(
                    new PostType
                    {
                        PostTypeTitle = "Новость",
                    },
                    new PostType
                    {
                        PostTypeTitle = "Статья",
                    }

                    );
                context.SaveChanges();


            }
        }
    }
}
