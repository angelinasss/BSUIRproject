using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using QuiqBlog.BusinessManagers;
using QuiqBlog.Data.Models;
using QuiqBlog.Models.AdminViewModels;
using QuiqBlog.Service.Interfaces;
using Xunit;
using Assert = NUnit.Framework.Assert;

[TestFixture]
public class AdminBusinessManagerTests
{
    private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
    private readonly Mock<IPostService> _mockPostService;
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IWebHostEnvironment> _mockWebHostEnvironment;
    private readonly AdminBusinessManager _adminBusinessManager;

    public AdminBusinessManagerTests()
    {
        _mockUserManager = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(),
            null, null, null, null, null, null, null, null);

        _mockPostService = new Mock<IPostService>();
        _mockUserService = new Mock<IUserService>();
        _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();

        _adminBusinessManager = new AdminBusinessManager(
            _mockUserManager.Object,
            _mockPostService.Object,
            _mockUserService.Object,
            _mockWebHostEnvironment.Object);
    }

    private Mock<UserManager<ApplicationUser>> userManagerMock;
    private Mock<IPostService> postServiceMock;
    private Mock<IUserService> userServiceMock;
    private Mock<IWebHostEnvironment> webHostEnvironmentMock;
    private AdminBusinessManager adminBusinessManager;

    [SetUp]
    public void Setup()
    {
        userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
        postServiceMock = new Mock<IPostService>();
        userServiceMock = new Mock<IUserService>();
        webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        adminBusinessManager = new AdminBusinessManager(userManagerMock.Object, postServiceMock.Object, userServiceMock.Object, webHostEnvironmentMock.Object);
    }

    [Test]
    public async Task GetAdminDashboard_Returns_IndexViewModel()
    {
        // Arrange
        var claimsPrincipal = new ClaimsPrincipal();
        var applicationUser = new ApplicationUser();
        userManagerMock.Setup(u => u.GetUserAsync(claimsPrincipal)).ReturnsAsync(applicationUser);
        postServiceMock.Setup(p => p.GetPosts(applicationUser)).Returns(new List<Post>());

        // Act
        var result = await adminBusinessManager.GetAdminDashboard(claimsPrincipal);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<IndexViewModel>(result);
    }

    [Test]
    public async Task GetAboutViewModel_Returns_AboutViewModel()
    {
        // Arrange
        var claimsPrincipal = new ClaimsPrincipal();
        var applicationUser = new ApplicationUser();
        userManagerMock.Setup(u => u.GetUserAsync(claimsPrincipal)).ReturnsAsync(applicationUser);

        // Act
        var result = await adminBusinessManager.GetAboutViewModel(claimsPrincipal);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<AboutViewModel>(result);
    }

    [Test]
    public async Task UpdateAbout_Updates_ApplicationUser()
    {
        // Arrange
        var claimsPrincipal = new ClaimsPrincipal();
        var aboutViewModel = new AboutViewModel
        {
            SubHeader = "Test SubHeader",
            Content = "Test Content",
            HeaderImage = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file")), 0, 0, "HeaderImage", "test.jpg")
        };
        var applicationUser = new ApplicationUser();
        userManagerMock.Setup(u => u.GetUserAsync(claimsPrincipal)).ReturnsAsync(applicationUser);
        webHostEnvironmentMock.Setup(w => w.WebRootPath).Returns("C:\\wwwroot");

        // Act
        await adminBusinessManager.UpdateAbout(aboutViewModel, claimsPrincipal);

        // Assert
        Assert.AreEqual(applicationUser.SubHeader, aboutViewModel.SubHeader);
        Assert.AreEqual(applicationUser.AboutContent, aboutViewModel.Content);
        userServiceMock.Verify(u => u.Update(applicationUser), Times.Once);
    }

    [Test]
    public async Task UpdateAbout_ShouldUpdateAboutContent()
    {
        // Arrange
        var claimsPrincipal = new ClaimsPrincipal();
        var applicationUser = new ApplicationUser();
        var aboutViewModel = new AboutViewModel
        {
            SubHeader = "New SubHeader",
            Content = "New About Content",
            HeaderImage = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file")), 0, 0, "test", "test.jpg")
        };

        _mockUserManager.Setup(x => x.GetUserAsync(claimsPrincipal))
            .ReturnsAsync(applicationUser);

        // Act
        await _adminBusinessManager.UpdateAbout(aboutViewModel, claimsPrincipal);

        // Assert
        Assert.AreEqual(aboutViewModel.SubHeader, applicationUser.SubHeader);
        Assert.AreEqual(aboutViewModel.Content, applicationUser.AboutContent);
    }
}