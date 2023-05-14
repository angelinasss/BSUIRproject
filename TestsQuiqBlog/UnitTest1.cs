using NUnit.Framework;

namespace TestsQuiqBlog
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

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
    }
}