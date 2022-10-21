using Core.DomainServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Portal.Controllers;
using Xunit;

namespace Portal.Tests;

public class UnitTest1
{
    // [Fact]
    public void Test1()
    {
        // Arrange
        var repositoryMock = new Mock<IGameNightRepository>();

        var controller = new GameNightController(repositoryMock.Object, null, null, null);
        
        // Act
        var result = controller.Organized() as ViewResult;

        // Assert
        Assert.Null(result.ViewName);
    }
}