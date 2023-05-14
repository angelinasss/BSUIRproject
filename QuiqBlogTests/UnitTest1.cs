using NUnit.Framework;
using Moq;
using QuiqBlog.BusinessManagers;
using QuiqBlog.Models;
using QuiqBlog.Services;
using PagedList;

namespace QuiqBlogTests
{
    public class PostBusinessManager
    {

        [Test]
        public async Task GetPostViewModel_Returns_PostViewModel()
        {
            // Arrange
            int? id = 1;
            var post = new Post { Id = 1, Title = "Test Post", Content = "Test Content", Published = true };
            var claimsPrincipal = new ClaimsPrincipal();
            var postServiceMock = new Mock<IPostService>();
            postServiceMock.Setup(p => p.GetPost(id.Value)).Returns(post);
            var authorizationServiceMock = new Mock<IAuthorizationService>();
            authorizationServiceMock.Setup(a => a.AuthorizeAsync(claimsPrincipal, post, Operations.Read)).ReturnsAsync(AuthorizationResult.Success());
            var postBusinessManager = new PostBusinessManager(null, postServiceMock.Object, null, authorizationServiceMock.Object);

            // Act
            var result = await postBusinessManager.GetPostViewModel(id, claimsPrincipal);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<PostViewModel>(result);
            Assert.AreEqual(post.Id, result.Post.Id);
        }
    }
}