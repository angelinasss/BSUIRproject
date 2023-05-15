using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PagedList.Core;
using QuiqBlog.BusinessManagers;
using QuiqBlog.BusinessManagers.Interfaces;
using QuiqBlog.Data.Models;
using QuiqBlog.Models.HomeViewModels;
using QuiqBlog.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace QuiqBlog.Tests.BusinessManagers
{
    [TestFixture]
    public class HomeBusinessManagerTests
    {
        private IHomeBusinessManager homeBusinessManager;
        private Mock<IPostService> postServiceMock;
        private Mock<IUserService> userServiceMock;
        private List<Post> posts;

        [SetUp]
        public void Setup()
        {
            postServiceMock = new Mock<IPostService>();
            userServiceMock = new Mock<IUserService>();
            homeBusinessManager = new HomeBusinessManager(postServiceMock.Object, userServiceMock.Object);
            posts = new List<Post> {
                new Post { Id = 1, Title = "Post 1", Published = true, Creator = new ApplicationUser { Id = "1" } },
                new Post { Id = 2, Title = "Post 2", Published = true, Creator = new ApplicationUser { Id = "1" } },
                new Post { Id = 3, Title = "Post 3", Published = false, Creator = new ApplicationUser { Id = "1" } },
                new Post { Id = 4, Title = "Post 4", Published = true, Creator = new ApplicationUser { Id = "2" } }
            };
        }

        [Test]
        public void GetAuthorViewModel_ReturnsBadRequestResult_WhenAuthorIdIsNull()
        {
            // Arrange
            string authorId = null;

            // Act
            var result = homeBusinessManager.GetAuthorViewModel(authorId, null, null);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public void GetAuthorViewModel_ReturnsNotFoundResult_WhenUserNotFound()
        {
            // Arrange
            string authorId = "1";
            userServiceMock.Setup(x => x.Get(authorId)).Returns((ApplicationUser)null);

            // Act
            var result = homeBusinessManager.GetAuthorViewModel(authorId, null, null);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public void GetAuthorViewModel_CallsPostServiceGetPosts_WithCorrectSearchString()
        {
            // Arrange
            string authorId = "1";
            string searchString = "post";
            var user = new ApplicationUser { Id = authorId };
            userServiceMock.Setup(x => x.Get(authorId)).Returns(user);

            // Act
            var result = homeBusinessManager.GetAuthorViewModel(authorId, searchString, null);

            // Assert
            postServiceMock.Verify(x => x.GetPosts(searchString), Times.Once);
        }

    }
}