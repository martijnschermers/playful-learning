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
        var gameRepoMock = new Mock<IGameRepository>(); 
        var userRepoMock = new Mock<IUserRepository>(); 
        var gameNightServiceMock = new Mock<IGameNightService>();

        var controller = new GameNightController(repositoryMock.Object, gameRepoMock.Object, userRepoMock.Object, gameNightServiceMock.Object);
        
        // Act
        var result = controller.Organized() as ViewResult;

        // Assert
        Assert.Null(result.ViewName);
    }
}