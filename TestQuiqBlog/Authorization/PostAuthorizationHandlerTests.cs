using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using QuiqBlog.Authorization;
using QuiqBlog.Data.Models;

namespace QuiqBlog.Tests
{
    [TestFixture]
    public class PostAuthorizationHandlerTests
    {
        private Mock<UserManager<ApplicationUser>> _mockUserManager;
        private ClaimsPrincipal _user;
        private Post _post;

        [SetUp]
        public void Setup()
        {
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "TestUser"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Email, "testuser@test.com")
            }, "mock"));

            _post = new Post
            {
                Creator = new ApplicationUser { Id = "1", UserName = "TestUser" },
                Published = false
            };
        }

        [Test]
        public async Task HandleRequirementAsync_DoesNotAllowUpdateOrDelete_WhenUserIsNotCreator()
        {
            // Arrange
            var requirement = new OperationAuthorizationRequirement { Name = Operations.Update.Name };
            var handler = new PostAuthorizationHandler(_mockUserManager.Object);

            _mockUserManager.Setup(x => x.GetUserAsync(_user)).ReturnsAsync(new ApplicationUser { Id = "2", UserName = "OtherUser" });

            // Act
            var context = new AuthorizationHandlerContext(new[] { requirement }, _user, _post);
            await handler.HandleAsync(context);

            // Assert
            Assert.That(context.HasSucceeded, Is.False);
        }

        [Test]
        public async Task HandleRequirementAsync_DoesNotAllowRead_WhenUserIsNotCreator()
        {
            // Arrange
            var requirement = new OperationAuthorizationRequirement { Name = Operations.Read.Name };
            var handler = new PostAuthorizationHandler(_mockUserManager.Object);

            _mockUserManager.Setup(x => x.GetUserAsync(_user)).ReturnsAsync(new ApplicationUser { Id = "2", UserName = "OtherUser" });

            // Act
            var context = new AuthorizationHandlerContext(new[] { requirement }, _user, _post);
            await handler.HandleAsync(context);

            // Assert
            Assert.That(context.HasSucceeded, Is.False);
        }
    }
}
