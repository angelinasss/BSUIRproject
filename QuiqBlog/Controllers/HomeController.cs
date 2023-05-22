using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework.Internal;
using QuiqBlog.BusinessManagers.Interfaces;
using QuiqBlog.Data;
using QuiqBlog.Data.Models;
using QuiqBlog.Models.HomeViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuiqBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostBusinessManager postBusinessManager;
        private readonly IHomeBusinessManager homeBusinessManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context,
            IPostBusinessManager blogBusinessManager,
            IHomeBusinessManager homeBusinessManager,
            ILogger<HomeController> logger,
            UserManager<ApplicationUser> userManager)
        {
            this.postBusinessManager = blogBusinessManager;
            this.homeBusinessManager = homeBusinessManager;
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [Route("/")]
        public IActionResult Index(string searchString, int? page)
        {
            return View(postBusinessManager.GetIndexViewModel(searchString, page));
        }

        public IActionResult BooksIndex(string searchString, int? page)
        {
            return View(postBusinessManager.GetIndexViewModel(searchString, page));
        }

        public IActionResult GamesIndex(string searchString, int? page)
        {
            return View(postBusinessManager.GetIndexViewModel(searchString, page));
        }

        public IActionResult FilmsIndex(string searchString, int? page)
        {
            return View(postBusinessManager.GetIndexViewModel(searchString, page));
        }

        public IActionResult MusicIndex(string searchString, int? page)
        {
            return View(postBusinessManager.GetIndexViewModel(searchString, page));
        }

        public IActionResult Author(string authorId, string searchString, int? page)
        {
            var actionResult = homeBusinessManager.GetAuthorViewModel(authorId, searchString, page);
            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }

        [Authorize]
        public async Task<IActionResult> Subscribe(string authorId)
        {
            try
            {
                // Получаем текущего пользователя
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser.Id == authorId)
                {
                    return Json(new { success = false });
                }

                // Проверяем, не подписан ли уже пользователь на данного автора
                var subscriptionExists = await _context.Subscriptions
                    .AnyAsync(s => s.SubscriberId == currentUser.Id && s.AuthorId == authorId);

                if (!subscriptionExists)
                {
                    // Создаем новую запись в таблице Subscription
                    var subscription = new Subscription
                    {
                        SubscriberId = currentUser.Id,
                        AuthorId = authorId
                    };
                    _context.Subscriptions.Add(subscription);

                    // Увеличиваем счетчик подписчиков у автора
                    var author = await _userManager.FindByIdAsync(authorId);
                    author.FollowersCount++;
                    await _userManager.UpdateAsync(author);

                    await _context.SaveChangesAsync();
                }
         


                // Возвращаем результат операции
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Записываем ошибку в лог
                _logger.LogError(ex, "Ошибка при подписке на автора");
                return Json(new { success = false });
            }
        }
    }
}