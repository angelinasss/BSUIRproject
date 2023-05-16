using System.Reflection;
using System;
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

        [Test]
        public void Create_ShouldHaveCorrectName()
        {
            // Arrange
            var operation = Operations.Create;

            // Assert
            Assert.IsNotNull(operation);
            Assert.AreEqual("Create", operation.Name);
        }

        [Test]
        public void Read_ShouldHaveCorrectName()
        {
            // Arrange
            var operation = Operations.Read;

            // Assert
            Assert.IsNotNull(operation);
            Assert.AreEqual("Read", operation.Name);
        }

        [Test]
        public void Update_ShouldHaveCorrectName()
        {
            // Arrange
            var operation = Operations.Update;

            // Assert
            Assert.IsNotNull(operation);
            Assert.AreEqual("Update", operation.Name);
        }

        [Test]
        public void Delete_ShouldHaveCorrectName()
        {
            // Arrange
            var operation = Operations.Delete;

            // Assert
            Assert.IsNotNull(operation);
            Assert.AreEqual("Delete", operation.Name);
        }

        [Test]
        public void Operations_Create_ShouldHaveExpectedName()
        {
            Assert.AreEqual(nameof(Create), Operations.Create.Name);
        }

        private object Create()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void Operations_Create_ShouldBePublic()
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.Static;
            var createField = typeof(Operations).GetField("Create", bindingFlags);
            Assert.IsNotNull(createField);
        }
        [Test]
        public void PostAuthorizationHandler_ShouldRequireCreateAccessForCreateOperation()
        {
            var operation = Operations.Create;
            var requirement = new OperationAuthorizationRequirement { Name = operation.Name };
            var handler = new PostAuthorizationHandler();

            var context = new AuthorizationHandlerContext(new[] { requirement }, null, null);
            handler.HandleAsync(context).Wait();

            Assert.IsFalse(context.HasSucceeded);
        }
    }
} 