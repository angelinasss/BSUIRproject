using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using QuiqBlog.BusinessManagers.Interfaces;
using QuiqBlog.Controllers;
using QuiqBlog.Models.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace QuiqBlog.Controllers.Tests
{
    [TestClass()]
    public class AdminControllerTests
    {

        [Test]
        public async Task Index_ReturnsCorrectView()
        {
            // Arrange
            var mockBusinessManager = new Mock<IAdminBusinessManager>();
            mockBusinessManager.Setup(m => m.GetAdminDashboard(It.IsAny<ClaimsPrincipal>()))
                               .ReturnsAsync(new IndexViewModel());

            var controller = new AdminController(mockBusinessManager.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
        new Claim(ClaimTypes.Name, "user1")
    }));

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOf<IndexViewModel>(viewResult.Model);
        }

        [Test]
        public async Task About_ReturnsViewWithAboutViewModel()
        {
            // Arrange
            var mockAdminBusinessManager = new Mock<IAdminBusinessManager>();
            var aboutViewModel = new AboutViewModel();
            mockAdminBusinessManager.Setup(x => x.GetAboutViewModel(It.IsAny<ClaimsPrincipal>()))
                                    .ReturnsAsync(aboutViewModel);
            var controller = new AdminController(mockAdminBusinessManager.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
        new Claim(ClaimTypes.Name, "testuser")
    }, "testauth"));

            // Act
            var result = await controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(aboutViewModel, result.ViewData.Model);
        }

        [Test]
        public async Task About_ReturnsCorrectView()
        {
            // Arrange
            var mockBusinessManager = new Mock<IAdminBusinessManager>();
            mockBusinessManager.Setup(m => m.GetAboutViewModel(It.IsAny<ClaimsPrincipal>()))
                               .ReturnsAsync(new AboutViewModel());

            var controller = new AdminController(mockBusinessManager.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
        new Claim(ClaimTypes.Name, "user1")
    }));

            // Act
            var result = await controller.About();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOf<AboutViewModel>(viewResult.Model);
        }

        [Test]
        public async Task UpdateAbout_CallsUpdateAboutOnAdminBusinessManager_AndRedirectsToAbout()
        {
            // Arrange
            var mockAdminBusinessManager = new Mock<IAdminBusinessManager>();
            var aboutViewModel = new AboutViewModel();
            var controller = new AdminController(mockAdminBusinessManager.Object);

            // Act
            var result = await controller.UpdateAbout(aboutViewModel) as RedirectToActionResult;

            // Assert
            mockAdminBusinessManager.Verify(x => x.UpdateAbout(aboutViewModel, controller.User), Times.Once);
            Assert.AreEqual("About", result.ActionName);
        }

        [Test]
        public async Task UpdateAbout_RedirectsToAboutView()
        {
            // Arrange
            var mockBusinessManager = new Mock<IAdminBusinessManager>();

            var controller = new AdminController(mockBusinessManager.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
        new Claim(ClaimTypes.Name, "user1")
    }));

            var viewModel = new AboutViewModel()
            {
                SubHeader = "New SubHeader",
                Content = "New Content"
            };

            // Act
            var result = await controller.UpdateAbout(viewModel);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("About", redirectResult.ActionName);
        }

        [Test]
        public async Task Index_RequiresAuthorization()
        {
            // Arrange
            var mockBusinessManager = new Mock<IAdminBusinessManager>();
            var controller = new AdminController(mockBusinessManager.Object);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Login", redirectResult.ActionName);
            Assert.AreEqual("Account", redirectResult.ControllerName);
        }

        [Test]
        public async Task About_RequiresAuthorization()
        {
            // Arrange
            var mockBusinessManager = new Mock<IAdminBusinessManager>();
            var controller = new AdminController(mockBusinessManager.Object);

            // Act
            var result = await controller.About();

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Login", redirectResult.ActionName);
            Assert.AreEqual("Account", redirectResult.ControllerName);
        }

        [Test]
        public async Task UpdateAbout_RequiresAuthorization()
        {
            // Arrange
            var mockBusinessManager = new Mock<IAdminBusinessManager>();
            var controller = new AdminController(mockBusinessManager.Object);

            // Act
            var result = await controller.UpdateAbout(new AboutViewModel());

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Login", redirectResult.ActionName);
            Assert.AreEqual("Account", redirectResult.ControllerName);
        }


        [Test]
        public async Task UnauthorizedUser_CannotAccessMethods()
        {
            // Arrange
            var mockBusinessManager = new Mock<IAdminBusinessManager>();
            var controller = new AdminController(mockBusinessManager.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            // Act
            var indexResult = await controller.Index();
            var aboutResult = await controller.About();
            var updateResult = await controller.UpdateAbout(new AboutViewModel());

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(indexResult);
            Assert.IsInstanceOf<RedirectToActionResult>(aboutResult);
            Assert.IsInstanceOf<RedirectToActionResult>(updateResult);
            var redirectResult = (RedirectToActionResult)indexResult;
            Assert.AreEqual("Login", redirectResult.ActionName);
            Assert.AreEqual("Account", redirectResult.ControllerName);
        }

        [Test]
        public async Task Index_CallsGetAdminDashboard()
        {
            // Arrange
            var mockBusinessManager = new Mock<IAdminBusinessManager>();
            mockBusinessManager.Setup(m => m.GetAdminDashboard(It.IsAny<ClaimsPrincipal>()))
                               .ReturnsAsync(new IndexViewModel());

            var controller = new AdminController(mockBusinessManager.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
        new Claim(ClaimTypes.Name, "user1")
    }));

            // Act
            var result = await controller.Index();

            // Assert
            mockBusinessManager.Verify(m => m.GetAdminDashboard(It.IsAny<ClaimsPrincipal>()), Times.Once);
        }

        [Test]
        public async Task About_CallsGetAboutViewModel()
        {
            // Arrange
            var mockBusinessManager = new Mock<IAdminBusinessManager>();
            mockBusinessManager.Setup(m => m.GetAboutViewModel(It.IsAny<ClaimsPrincipal>()))
                               .ReturnsAsync(new AboutViewModel());

            var controller = new AdminController(mockBusinessManager.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
        new Claim(ClaimTypes.Name, "user1")
    }));

            // Act
            var result = await controller.About();

            // Assert
            mockBusinessManager.Verify(m => m.GetAboutViewModel(It.IsAny<ClaimsPrincipal>()), Times.Once);
        }

        [Test]
        public async Task UpdateAbout_CallsUpdateAbout()
        {
            // Arrange
            var mockBusinessManager = new Mock<IAdminBusinessManager>();

            var controller = new AdminController(mockBusinessManager.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
        new Claim(ClaimTypes.Name, "user1")
    }));

            var viewModel = new AboutViewModel()
            {
                SubHeader = "New SubHeader",
                Content = "New Content"
            };

            // Act
            var result = await controller.UpdateAbout(viewModel);

            // Assert
            mockBusinessManager.Verify(m => m.UpdateAbout(It.IsAny<AboutViewModel>(), It.IsAny<ClaimsPrincipal>()), Times.Once);
        }
    }
}