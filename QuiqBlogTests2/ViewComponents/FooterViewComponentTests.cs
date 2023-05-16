using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
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
    public class FooterViewComponentTests
    {
        [Test]
        public async Task FooterViewComponent_ReturnsNonEmptyResult()
        {
            // Arrange
            FooterViewComponent viewComponent = new FooterViewComponent();

            // Act
            var result = await viewComponent.InvokeAsync();

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
        }
    }
}