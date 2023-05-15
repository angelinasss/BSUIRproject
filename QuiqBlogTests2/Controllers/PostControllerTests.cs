using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using QuiqBlog.BusinessManagers.Interfaces;
using QuiqBlog.Controllers;
using QuiqBlog.Data.Models;
using QuiqBlog.Models.PostViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace QuiqBlog.Tests.Controllers
{
    [TestFixture]
    public class PostControllerTests
    {
        private Mock<IPostBusinessManager> postBusinessManagerMock;
        private Mock<ClaimsPrincipal> userMock;
        private PostController postController;

        [SetUp]
        public void Setup()
        {
            postBusinessManagerMock = new Mock<IPostBusinessManager>();
            userMock = new Mock<ClaimsPrincipal>();
            postController = new PostController(postBusinessManagerMock.Object);
            postController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = userMock.Object
                }
            };
        }

        [Test]
        public async Task Index_ReturnsViewResult_WhenPostExists()
        {
            // Arrange
            int postId = 1;
            var postViewModel = new PostViewModel { Post = new Post { Id = postId } };
            postBusinessManagerMock.Setup(x => x.GetPostViewModel(postId, userMock.Object))
                                   .ReturnsAsync(new ActionResult<PostViewModel>(postViewModel));

            // Act
            var result = await postController.Index(postId);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.AreEqual(postViewModel, viewResult.Model);
        }

        [Test]
        public async Task Index_ReturnsActionResult_WhenPostDoesNotExist()
        {
            // Arrange
            int postId = 1;
            postBusinessManagerMock.Setup(x => x.GetPostViewModel(postId, userMock.Object))
                                   .ReturnsAsync(new ActionResult<PostViewModel>(new NotFoundResult()));

            // Act
            var result = await postController.Index(postId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Create_ReturnsViewResult()
        {
            // Act
            var result = postController.Create();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task Edit_ReturnsViewResult_WhenPostExists()
        {
            // Arrange
            int postId = 1;
            var editViewModel = new EditViewModel { Post = new Post { Id = postId } };
            postBusinessManagerMock.Setup(x => x.GetEditViewModel(postId, userMock.Object))
                                   .ReturnsAsync(new ActionResult<EditViewModel>(editViewModel));

            // Act
            var result = await postController.Edit(postId);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.AreEqual(editViewModel, viewResult.Model);
        }

        [Test]
        public async Task Edit_ReturnsActionResult_WhenPostDoesNotExist()
        {
            // Arrange
            int postId = 1;
            postBusinessManagerMock.Setup(x => x.GetEditViewModel(postId, userMock.Object))
                                   .ReturnsAsync(new ActionResult<EditViewModel>(new NotFoundResult()));

            // Act
            var result = await postController.Edit(postId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}