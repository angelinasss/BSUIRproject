using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using QuiqBlog.Authorization;
using QuiqBlog.BusinessManagers;
using QuiqBlog.Controllers;
using QuiqBlog.Data.Models;
using QuiqBlog.Models.HomeViewModels;
using QuiqBlog.Models.PostViewModels;
using QuiqBlog.Service.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace QuiqBlog.Tests.BusinessManagers
{
    [TestFixture]
    public class PostBusinessManagerTests
    {

        [Test]
        public void GetIndexViewModel_Returns_IndexViewModel()
        {
            // Arrange
            var searchString = "test";
            int? page = 1;
            var posts = new List<Post> {
                new Post { Id = 1, Title = "Test Post 1", Content = "Test Content 1", Published = true },
                new Post { Id = 2, Title = "Test Post 2", Content = "Test Content 2", Published = true },
                new Post { Id = 3, Title = "Test Post 3", Content = "Test Content 3", Published = true }
            };
            var postServiceMock = new Mock<IPostService>();
            postServiceMock.Setup(p => p.GetPosts(searchString)).Returns(posts);
            var postBusinessManager = new PostBusinessManager(null, postServiceMock.Object, null, null);

            // Act
            var result = postBusinessManager.GetIndexViewModel(searchString, page);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IndexViewModel>(result);
            Assert.AreEqual(posts.Count, result.Posts.Count);
            Assert.AreEqual(searchString, result.SearchString);
            Assert.AreEqual(page, result.PageNumber);
        }

       
        [Test]
        public async Task CreatePost_Returns_Post()
        {
            // Arrange
            var createViewModel = new CreateViewModel
            {
                Post = new Post { Title = "Test Post", Content = "Test Content" },
                HeaderImage = new Mock<IFormFile>().Object
            };
            var claimsPrincipal = new ClaimsPrincipal();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(u => u.GetUserAsync(claimsPrincipal)).ReturnsAsync(new ApplicationUser());
            var postServiceMock = new Mock<IPostService>();
            postServiceMock.Setup(p => p.Add(It.IsAny<Post>())).ReturnsAsync(new Post { Id = 1, Title = "Test Post", Content = "Test Content" });
            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            var postBusinessManager = new PostBusinessManager(userManagerMock.Object, postServiceMock.Object, webHostEnvironmentMock.Object, null);

            // Act
            var result = await postBusinessManager.CreatePost(createViewModel, claimsPrincipal);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Post>(result);
            Assert.AreEqual(createViewModel.Post.Title, result.Title);
            Assert.AreEqual(createViewModel.Post.Content, result.Content);
        }

        [Test]
        public async Task CreateComment_Returns_Comment()
        {
            // Arrange
            var postViewModel = new PostViewModel
            {
                Post = new Post { Id = 1, Title = "Test Post", Content = "Test Content" },
                Comment = new Comment { Content = "Test Comment" }
            };
            var claimsPrincipal = new ClaimsPrincipal();
            var postServiceMock = new Mock<IPostService>();
            postServiceMock.Setup(p => p.GetPost(postViewModel.Post.Id)).Returns(new Post { Id = 1, Title = "Test Post", Content = "Test Content" });
            postServiceMock.Setup(p => p.Add(It.IsAny<Comment>())).ReturnsAsync(new Comment { Id = 1, Content = "Test Comment" });
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(u => u.GetUserAsync(claimsPrincipal)).ReturnsAsync(new ApplicationUser());
            var postBusinessManager = new PostBusinessManager(userManagerMock.Object, postServiceMock.Object, null, null);

            // Act
            var result = await postBusinessManager.CreateComment(postViewModel, claimsPrincipal);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Comment>(result.Value);
            Assert.AreEqual(postViewModel.Comment.Content, result.Value.Content);
        }
    }
}
