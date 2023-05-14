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

        //[Test]
        //public async Task UpdatePost_Returns_ActionResult()
        //{
        //    // Arrange
        //    var editViewModel = new EditViewModel
        //    {
        //        Post = new Post { Id = 1, Title = "Test Post", Content = "Test Content" }
        //    };
        //    var claimsPrincipal = new ClaimsPrincipal();
        //    var postServiceMock = new Mock<IPostService>();
        //    postServiceMock.Setup(p => p.GetPost(editViewModel.Post.Id)).Returns(new Post { Id = 1, Title = "Test Post", Content = "Test Content" });
        //    var authorizationServiceMock = new Mock<IAuthorizationService>();
        //    authorizationServiceMock.Setup(a => a.AuthorizeAsync(claimsPrincipal, It.IsAny<Post>(), Operations.Update)).ReturnsAsync(AuthorizationResult.Success());
        //    var postBusinessManager = new PostBusinessManager(null, postServiceMock.Object, null, authorizationServiceMock.Object);

        //    // Act
        //    var result = await postBusinessManager.UpdatePost(editViewModel, claimsPrincipal);

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<ActionResult>(result);
        //}

        //[Test]
        //public async Task GetPostViewModel_Returns_PostViewModel()
        //{
        //    // Arrange
        //    int? id = 1;
        //    var post = new Post { Id = 1, Title = "Test Post", Content = "Test Content", Published = true };
        //    var claimsPrincipal = new ClaimsPrincipal();
        //    var postServiceMock = new Mock<IPostService>();
        //    postServiceMock.Setup(p => p.GetPost(id.Value)).Returns(post);
        //    var authorizationServiceMock = new Mock<IAuthorizationService>();
        //    authorizationServiceMock.Setup(a => a.AuthorizeAsync(claimsPrincipal, post, Operations.Read)).ReturnsAsync(AuthorizationResult.Success());
        //    var postBusinessManager = new PostBusinessManager(null, postServiceMock.Object, null, authorizationServiceMock.Object);

        //    // Act
        //    var result = await postBusinessManager.GetPostViewModel(id, claimsPrincipal);

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<PostViewModel>(result);
        //    Assert.AreEqual(post.Id, result.Post.Id);
        //}
    }
}
