using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuiqBlog.Data;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Assert = NUnit.Framework.Assert;

namespace QuiqBlog.Tests.Data
{
    [TestFixture]
    public class ApplicationDbContextTests
    {
        [Test]
        public void ApplicationDbContext_CanCreateInstance()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .Options;

            // Act
            var dbContext = new ApplicationDbContext(options);

            // Assert
            Assert.IsNotNull(dbContext);
        }
        [Test]
        public void ApplicationDbContext_DbSetsAreNotNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .Options;

            // Act
            var dbContext = new ApplicationDbContext(options);

            // Assert
            Assert.IsNotNull(dbContext.Posts);
            Assert.IsNotNull(dbContext.Comments);
            Assert.IsNotNull(dbContext.Likes);
        }
        [Test]
        public void ApplicationDbContext_OnModelCreating_IsCalled()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .Options;

            // Act
            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Database.EnsureCreated();
            }

        }
    }
}