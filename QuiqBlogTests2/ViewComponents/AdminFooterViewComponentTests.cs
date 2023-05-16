using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using QuiqBlog.ViewComponents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace QuiqBlog.ViewComponents.Tests
{
    [TestClass()]
    public class AdminFooterViewComponentTests
    {
        [Test]
        public async Task AdminFooterViewComponent_ReturnsNonEmptyResult()
        {
            // Arrange
            AdminFooterViewComponent viewComponent = new AdminFooterViewComponent();

            // Act
            var result = await viewComponent.InvokeAsync();

            // Assert
            NUnit.Framework.Assert.IsNotNull(result);
        }
        [Test]
        public async Task AdminFooterViewComponent_ReturnsExpectedResultType()
        {
            // Arrange
            AdminFooterViewComponent viewComponent = new AdminFooterViewComponent();

            // Act
            var result = await viewComponent.InvokeAsync();

            // Assert
            Assert.IsInstanceOf<IViewComponentResult>(result);
        }
    }
}