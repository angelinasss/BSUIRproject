using QuiqBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuiqBlog.Data
{
    public static class CategoryInitialize
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category
                    {
                        Title = "Music",
                        Description = "Something about new sounds"
                    },
                    new Category
                    {
                        Title = "Films",
                        Description = "Everything about films"
                    },
                    new Category
                    {
                        Title = "Books",
                        Description = "Oh, you seem to be an avid reader"
                    },
                    new Category
                    {
                        Title = "Games",
                        Description = "Everything about the world of gaming"
                    }

                    );
                context.SaveChanges();


            }
        }
    }
}
