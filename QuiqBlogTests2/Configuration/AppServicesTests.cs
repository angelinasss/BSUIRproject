using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using QuiqBlog.BusinessManagers.Interfaces;
using QuiqBlog.Configuration;
using QuiqBlog.Data;
using QuiqBlog.Data.Models;
using QuiqBlog.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuiqBlog.Configuration.Tests
{
    [TestClass()]
    public class AppServicesTests
    {
        [Test]
        public void AddCustomServices_RegistersAllBusinessManagersAndServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddCustomServices();

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(services.Any(x => x.ServiceType == typeof(IPostBusinessManager)));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(services.Any(x => x.ServiceType == typeof(IAdminBusinessManager)));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(services.Any(x => x.ServiceType == typeof(IHomeBusinessManager)));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(services.Any(x => x.ServiceType == typeof(IPostService)));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(services.Any(x => x.ServiceType == typeof(IUserService)));
        }
        [Test]
        public void AddCustomAuthorization_RegistersAuthorizationHandler()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddCustomAuthorization();

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(services.Any(x => x.ServiceType == typeof(IAuthorizationHandler)));
        }
    }
}