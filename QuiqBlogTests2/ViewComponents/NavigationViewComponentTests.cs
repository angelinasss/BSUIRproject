using NUnit.Framework;
using QuiqBlog.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace QuiqBlog.ViewComponents.Tests
{
    [TestFixture]
    public class NavigationViewComponentTests
    {
        [Test]
        public async Task InvokeAsync_ReturnsViewComponentResult()
        {
            // Arrange
            var viewComponent = new NavigationViewComponent();

            // Act
            var result = await viewComponent.InvokeAsync();

            // Assert
            Assert.IsInstanceOf<ViewViewComponentResult>(result);
        }
    }
}