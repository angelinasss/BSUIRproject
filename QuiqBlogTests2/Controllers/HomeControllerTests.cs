using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using QuiqBlog.BusinessManagers.Interfaces;
using QuiqBlog.Data;
using QuiqBlog.Data.Models;
using QuiqBlog.Models.AdminViewModels;
using QuiqBlog.Models.HomeViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace QuiqBlog.Controllers.Tests
{
    [TestFixture]
    public class HomeControllerTests
    {
        private readonly ApplicationDbContext context;
        private readonly IPostBusinessManager postBusinessManager;
        private readonly IHomeBusinessManager homeBusinessManager;
        private readonly ILogger<HomeController> logger;
        private readonly UserManager<ApplicationUser> userManager;

        //public HomeControllerTests()
        //{
        //    context = new ApplicationDbContext(
        //        new DbContextOptionsBuilder<ApplicationDbContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //        .Options);
        //    postBusinessManager = Mock.Of<IPostBusinessManager>();
        //    homeBusinessManager = Mock.Of<IHomeBusinessManager>();
        //    logger = Mock.Of<ILogger<HomeController>>();
        //    userManager = Mock.Of<UserManager<ApplicationUser>>();
        //}

        //[Test]
        //public void Index_ReturnsViewWithIndexViewModel()
        //{
        //    //Arrange
        //    var controller = new HomeController(context, postBusinessManager, homeBusinessManager, logger, userManager);

        //    //Act
        //    var result = controller.Index(null, null) as ViewResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<IndexViewModel>(result.Model);
        //}

        //[Test]
        //public async Task Author_ReturnsViewWithAuthorViewModel_WhenAuthorExists()
        //{
        //    //Arrange
        //    var controller = new HomeController(context, postBusinessManager, homeBusinessManager, logger, userManager);
        //    var authorId = "1";
        //    var searchString = "test";
        //    var page = 1;
        //    var authorViewModel = new AuthorViewModel();

        //    Mock.Get(homeBusinessManager)
        //        .Setup(x => x.GetAuthorViewModel(authorId, searchString, page))
        //        .Returns(Task.FromResult<ActionResult>(new ViewResult { ViewName = "Author", ViewData = new ViewDataDictionary(authorViewModel) }));

        //    //Act
        //    var result = await controller.Author(authorId, searchString, page) as ViewResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual("Author", result.ViewName);
        //    Assert.IsInstanceOf<AuthorViewModel>(result.Model);
        //}

        //[Test]
        //public async Task Author_ReturnsNotFound_WhenAuthorDoesNotExist()
        //{
        //    //Arrange
        //    var controller = new HomeController(context, postBusinessManager, homeBusinessManager, logger, userManager);
        //    var authorId = "1";
        //    var searchString = "test";
        //    var page = 1;

        //    Mock.Get(homeBusinessManager)
        //        .Setup(x => x.GetAuthorViewModel(authorId, searchString, page))
        //        .Returns(Task.FromResult<ActionResult>(new NotFoundResult()));

        //    //Act
        //    var result = await controller.Author(authorId, searchString, page) as NotFoundResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //}

        //[Test]
        //public async Task Subscribe_ReturnsJsonSuccess_WhenSubscriptionCreated()
        //{
        //    //Arrange
        //    var controller = new HomeController(context, postBusinessManager, homeBusinessManager, logger, userManager);
        //    var authorId = "1";
        //    var currentUser = new ApplicationUser { Id = "2" };
        //    var subscriptionExists = false;

        //    var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, currentUser.Id) };
        //    var identity = new ClaimsIdentity(claims);
        //    var user = new ClaimsPrincipal(identity);
        //    controller.ControllerContext = new ControllerContext
        //    {
        //        HttpContext = new DefaultHttpContext { User = user }
        //    };

        //    Mock.Get(userManager)
        //        .Setup(x => x.GetUserAsync(user))
        //        .Returns(Task.FromResult(currentUser));
        //    Mock.Get(context.Subscriptions)
        //        .Setup(x => x.AnyAsync(s => s.SubscriberId == currentUser.Id && s.AuthorId == authorId))
        //        .Returns(Task.FromResult(subscriptionExists));
        //    Mock.Get(userManager)
        //        .Setup(x => x.FindByIdAsync(authorId))
        //        .Returns(Task.FromResult(new ApplicationUser { FollowersCount = 0 }));
        //    Mock.Get(userManager)
        //        .Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>()))
        //        .Returns(Task.FromResult(IdentityResult.Success));

        //    //Act
        //    var result = await controller.Subscribe(authorId) as JsonResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual("{\"success\":true}", result.Value.ToString());
        //}

        //[Test]
        //public async Task Subscribe_ReturnsJsonSuccess_WhenSubscriptionAlreadyExists()
        //{
        //    //Arrange
        //    var controller = new HomeController(context, postBusinessManager, homeBusinessManager, logger, userManager);
        //    var authorId = "1";
        //    var currentUser = new ApplicationUser { Id = "2" };
        //    var subscriptionExists = true;

        //    var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, currentUser.Id) };
        //    var identity = new ClaimsIdentity(claims);
        //    var user = new ClaimsPrincipal(identity);
        //    controller.ControllerContext = new ControllerContext
        //    {
        //        HttpContext = new DefaultHttpContext { User = user }
        //    };

        //    Mock.Get(userManager)
        //        .Setup(x => x.GetUserAsync(user))
        //        .Returns(Task.FromResult(currentUser));
        //    Mock.Get(context.Subscriptions)
        //        .Setup(x => x.AnyAsync(s => s.SubscriberId == currentUser.Id && s.AuthorId == authorId))
        //        .Returns(Task.FromResult(subscriptionExists));

        //    //Act
        //    var result = await controller.Subscribe(authorId) as JsonResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual("{\"success\":false}", result.Value.ToString());
        //}

        //[Test]
        //public async Task Subscribe_ReturnsJsonSuccess_WhenSubscriptionCreatedAndAuthorFollowersCountIncreased()
        //{
        //    //Arrange
        //    var controller = new HomeController(context, postBusinessManager, homeBusinessManager, logger, userManager);
        //    var authorId = "1";
        //    var currentUser = new ApplicationUser { Id = "2" };
        //    var subscriptionExists = false;

        //    var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, currentUser.Id) };
        //    var identity = new ClaimsIdentity(claims);
        //    var user = new ClaimsPrincipal(identity);
        //    controller.ControllerContext = new ControllerContext
        //    {
        //        HttpContext = new DefaultHttpContext { User = user }
        //    };

        //    Mock.Get(userManager)
        //        .Setup(x => x.GetUserAsync(user))
        //        .Returns(Task.FromResult(currentUser));
        //    Mock.Get(context.Subscriptions)
        //        .Setup(x => x.AnyAsync(s => s.SubscriberId == currentUser.Id && s.AuthorId == authorId))
        //        .Returns(Task.FromResult(subscriptionExists));
        //    Mock.Get(userManager)
        //        .Setup(x => x.FindByIdAsync(authorId))
        //        .Returns(Task.FromResult(new ApplicationUser { FollowersCount = 0 }));
        //    Mock.Get(userManager)
        //        .Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>()))
        //        .Returns(Task.FromResult(IdentityResult.Success));
        //    Mock.Get(context)
        //        .Setup(x => x.SaveChangesAsync())
        //        .Returns(Task.FromResult(1));

        //    //Act
        //    var result = await controller.Subscribe(authorId) as JsonResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual("{\"success\":true}", result.Value.ToString());
        //    Mock.Get(userManager)
        //        .Verify(x => x.UpdateAsync(It.Is<ApplicationUser>(a => a.FollowersCount == 1)), Times.Once);
        //}

        //[Test]
        //public async Task Subscribe_ReturnsJsonError_WhenExceptionThrown()
        //{
        //    //Arrange
        //    var controller = new HomeController(context, postBusinessManager, homeBusinessManager, logger, userManager);
        //    var authorId = "1";
        //    var currentUser = new ApplicationUser { Id = "2" };

        //    var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, currentUser.Id) };
        //    var identity = new ClaimsIdentity(claims);
        //    var user = new ClaimsPrincipal(identity);
        //    controller.ControllerContext = new ControllerContext
        //    {
        //        HttpContext = new DefaultHttpContext { User = user }
        //    };

        //    Mock.Get(userManager)
        //        .Setup(x => x.GetUserAsync(user))
        //        .Throws(new Exception());

        //    //Act
        //    var result = await controller.Subscribe(authorId) as JsonResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual("{\"success\":false}", result.Value.ToString());
        //}

        [Test]
        public void Author_ReturnsViewWithAuthorViewModel_WhenGetAuthorViewModelReturnsValue()
        {
            // Arrange
            var mockPostBusinessManager = new Mock<IPostBusinessManager>();
            var mockHomeBusinessManager = new Mock<IHomeBusinessManager>();
            var authorViewModel = new AuthorViewModel();
            mockHomeBusinessManager.Setup(x => x.GetAuthorViewModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>()))
                                    .Returns(new ActionResult<AuthorViewModel>(authorViewModel));
            var controller = new HomeController(mockPostBusinessManager.Object, mockHomeBusinessManager.Object);

            // Act
            var result = controller.Author("testauthor", null, null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(authorViewModel, result.ViewData.Model);
        }

        [Test]
        public void Author_ReturnsActionResult_WhenGetAuthorViewModelReturnsActionResult()
        {
            // Arrange
            var mockPostBusinessManager = new Mock<IPostBusinessManager>();
            var mockHomeBusinessManager = new Mock<IHomeBusinessManager>();
            var actionResult = new RedirectToActionResult("TestAction", "TestController", null);
            mockHomeBusinessManager.Setup(x => x.GetAuthorViewModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>()))
                                    .Returns(new ActionResult<AuthorViewModel>(actionResult));
            var controller = new HomeController(mockPostBusinessManager.Object, mockHomeBusinessManager.Object);

            // Act
            var result = controller.Author("testauthor", null, null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(actionResult, result);
        }

    }
}