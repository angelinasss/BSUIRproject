using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using QuiqBlog.Authorization;
using QuiqBlog.Data.Models;
using Xunit;

namespace QuiqBlog.Authorization.Tests
{
    [TestFixture]
    public class OperationsTests
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

        [Fact]
        public void Create_ShouldHaveCorrectName()
        {
            // Arrange
            var operation = Operations.Create;

            // Assert
            Assert.IsNotNull(operation);
            Assert.Equals("Create", operation.Name);
        }

        [Fact]
        public void Read_ShouldHaveCorrectName()
        {
            // Arrange
            var operation = Operations.Read;

            // Assert
            Assert.IsNotNull(operation);
            Assert.Equals("Read", operation.Name);
        }

        [Fact]
        public void Update_ShouldHaveCorrectName()
        {
            // Arrange
            var operation = Operations.Update;

            // Assert
            Assert.IsNotNull(operation);
            Assert.Equals("Update", operation.Name);
        }

        [Fact]
        public void Delete_ShouldHaveCorrectName()
        {
            // Arrange
            var operation = Operations.Delete;

            // Assert
            Assert.IsNotNull(operation);
            Assert.Equals("Delete", operation.Name);
        }
    }
} 