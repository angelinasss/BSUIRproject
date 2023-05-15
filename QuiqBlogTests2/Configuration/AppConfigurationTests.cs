using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Moq;
using NUnit.Framework;
using QuiqBlog.Authorization;
using QuiqBlog.Data.Models;
using Xunit;

namespace QuiqBlog.Configuration.Tests
    {
        public class AppConfigurationTests
        {
            [Fact]
            public void AddDefaultConfiguration_ShouldConfigureApplication()
            {
                // Arrange
                var applicationBuilderMock = new Mock<IApplicationBuilder>();
                var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
                webHostEnvironmentMock.Setup(x => x.IsDevelopment()).Returns(false);

                // Act
                AppConfiguration.AddDefaultConfiguration(applicationBuilderMock.Object, webHostEnvironmentMock.Object);

                // Assert
                applicationBuilderMock.Verify(x => x.UseDeveloperExceptionPage(), Times.Once);
                applicationBuilderMock.Verify(x => x.UseHsts(), Times.Once);
                applicationBuilderMock.Verify(x => x.UseHttpsRedirection(), Times.Once);
                applicationBuilderMock.Verify(x => x.UseStaticFiles(), Times.Once);
                applicationBuilderMock.Verify(x => x.UseRouting(), Times.Once);
                applicationBuilderMock.Verify(x => x.UseAuthentication(), Times.Once);
                applicationBuilderMock.Verify(x => x.UseAuthorization(), Times.Once);
                applicationBuilderMock.Verify(x => x.UseEndpoints(It.IsAny<Action<IEndpointRouteBuilder>>()), Times.Once);
            }
        }
    }

namespace QuiqBlog.Tests.Configuration
{

}