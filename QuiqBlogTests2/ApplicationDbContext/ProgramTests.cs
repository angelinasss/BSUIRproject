using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using QuiqBlog;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using QuiqBlog.Configuration;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace QuiqBlogTests.ApplicationDbContext
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void CreateHostBuilder_ReturnsIHostBuilder()
        {
            // Arrange
            var args = new string[] { };

            // Act
            var hostBuilder = Program.CreateHostBuilder(args);

            // Assert
            Assert.IsInstanceOfType(hostBuilder, typeof(HostBuilder));
        }
    }
}