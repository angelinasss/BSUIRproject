using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using NUnit.Framework;
using System.Threading.Tasks;

namespace QuiqBlog.ViewComponents.Tests
{
    [TestFixture]
    public class HeaderViewComponentTests
    {
        [Test]
        public async Task InvokeAsync_ReturnsViewComponentResult()
        {
            // Arrange
            var viewComponent = new HeaderViewComponent();

            // Act
            var result = await viewComponent.InvokeAsync();

            // Assert
            Assert.IsInstanceOf<ViewViewComponentResult>(result);
        }
    }
}