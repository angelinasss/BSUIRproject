﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using QuiqBlog.ViewComponents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuiqBlog.ViewComponents.Tests
{
    [TestClass()]
    public class AdminSideNavViewComponentTests
    {
        [Test]
        public async Task AdminSideNavViewComponent_ReturnsNonEmptyResult()
        {
            // Arrange
            AdminSideNavViewComponent viewComponent = new AdminSideNavViewComponent();

            // Act
            var result = await viewComponent.InvokeAsync();

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
        }
    }
}