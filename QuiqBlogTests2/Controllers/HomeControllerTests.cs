using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using QuiqBlog.BusinessManagers.Interfaces;
using QuiqBlog.Controllers;
using QuiqBlog.Data.Models;
using QuiqBlog.Data;
using QuiqBlog.Models.HomeViewModels;
using System;
using System.Threading.Tasks;

[TestFixture]
public class HomeControllerTests
{
    private Mock<IPostBusinessManager> postBusinessManagerMock;
    private Mock<IHomeBusinessManager> homeBusinessManagerMock;
    private Mock<UserManager<ApplicationUser>> userManagerMock;
    private Mock<ApplicationDbContext> contextMock;
    private Mock<ILogger<HomeController>> loggerMock;
    private HomeController controller;

    [SetUp]
    public void Setup()
    {
        postBusinessManagerMock = new Mock<IPostBusinessManager>();
        homeBusinessManagerMock = new Mock<IHomeBusinessManager>();
        userManagerMock = new Mock<UserManager<ApplicationUser>>();
        contextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
        loggerMock = new Mock<ILogger<HomeController>>();
        controller = new HomeController(contextMock.Object, postBusinessManagerMock.Object, homeBusinessManagerMock.Object, loggerMock.Object, userManagerMock.Object);
    }

}