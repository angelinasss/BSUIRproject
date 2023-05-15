using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using QuiqBlog.BusinessManagers.Interfaces;
using QuiqBlog.Controllers;
using System.Threading.Tasks;
using System.Security.Claims;
using QuiqBlog.Models.PostViewModels;
using QuiqBlog.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Assert = NUnit.Framework.Assert;

namespace Blog.Controllers.Tests
{
    [TestFixture]
    public class PostControllerTests
    {
        private Mock<IPostBusinessManager> postBusinessManagerMock;
        private PostController postController;

        [SetUp]
        public void Setup()
        {
            postBusinessManagerMock = new Mock<IPostBusinessManager>();
            postController = new PostController(postBusinessManagerMock.Object);
        }

        [Test]
        public void Create_ReturnsViewResult()
        {
            // Arrange

            // Act
            var result = postController.Create();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task Add_RedirectsToCreateAction()
        {
            // Arrange
            var createViewModel = new CreateViewModel();

            // Act
            var result = await postController.Add(createViewModel);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.AreEqual("Create", ((RedirectToActionResult)result).ActionName);
        }
      
    }
}