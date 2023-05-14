using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuiqBlog.Data.Models;
using QuiqBlog.Data;
using System.Security.Claims;
using System.Linq;
using QuiqBlog.Models.PostViewModels;
using Microsoft.Extensions.Hosting;
using QuiqBlog.Data.Migrations;

namespace Blog.Controllers
{
    [Authorize]
    public class LikeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LikeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Vote(int postId, bool isLiked, bool isDisliked)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            // Проверяем, голосовал ли пользователь ранее за этот пост
            var like = _context.Likes.FirstOrDefault(l => l.PostId == postId && l.UserId == userId);

            if (like != null && like.IsLiked == isLiked && like.IsDisliked == isDisliked)
            {
                // Если пользователь уже голосовал и его предыдущий голос совпадает с текущим,
                // то удаляем его голос
                _context.Likes.Remove(like);

                var post = _context.Posts.FirstOrDefault(p => p.Id == postId);
                if (post != null)
                {
                    if (isLiked)
                    {
                        post.LikesCount--;
                    }
                    else if (isDisliked)
                    {
                        post.DislikesCount--;
                    }
                    _context.SaveChanges();
                }
            }
            else if(like == null)
            {
                // Если пользователь голосует в первый раз или меняет свой голос,
                // то создаем новый объект лайка
                if (like != null)
                {
                    like.IsLiked = isLiked;
                    like.IsDisliked = isDisliked;
                }
                else
                {
                    like = new Like
                    {
                        PostId = postId,
                        UserId = userId
                    };
                }

                like.IsLiked = isLiked;
                like.IsDisliked = isDisliked;

                var post = _context.Posts.FirstOrDefault(p => p.Id == postId);
                if (post != null)
                {
                    if (isLiked)
                    {
                        post.LikesCount++;
                        if (like != null && like.IsDisliked)
                        {
                            post.DislikesCount--;
                        }
                    }
                    else if (isDisliked)
                    {
                        post.DislikesCount++;
                        if (like != null && like.IsLiked)
                        {
                            post.LikesCount--;
                        }
                    }
                    _context.SaveChanges();

                    if (like.Id == 0)
                    {
                        _context.Likes.Add(like);
                    }
                }
            }
            else 
            {
                // Если пользователь пытается поставить и лайк и дизлайк или не ставит ни одного из них,
                // то ничего не делаем
            }
            _context.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
