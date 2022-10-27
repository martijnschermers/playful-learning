using System.Collections.Generic;
using Core.Domain;
using Core.DomainServices.Repositories.Interface;
using Moq;
using Portal.Controllers;
using Xunit;

namespace Core.DomainServices.Tests;

public class GameRepositoryTest
{
    [Fact]
    public void GetGameById_Returns_The_Correct_Game()
    {
        // Arrange
        var repositoryMock = new Mock<IGameRepository>();
        repositoryMock
            .Setup(r => r.GetGameById(1))
            .Returns(new Game { Id = 1, Name = "Uno" });

        var controller = new GameController(repositoryMock.Object);

        // Act
        var gameNight = controller.GetGameById(1);

        // Assert
        repositoryMock.Verify(r => r.GetGameById(1));
        Assert.Equal("Uno", gameNight.Name);
    }

    [Fact]
    public void GetAllGames_Returns_All_Games()
    {
        var game1 = new Game { Id = 1, Name = "Uno", Description = "Leuk spel" };
        var game2 = new Game { Id = 2, Name = "Monopoly", Description = "Leuk spel" };

        // Arrange
        var repositoryMock = new Mock<IGameRepository>();
        repositoryMock
            .Setup(r => r.GetAllGames())
            .Returns(new List<Game> { game1, game2 });

        var controller = new GameController(repositoryMock.Object);

        // Act
        var games = controller.GetAllGames();

        // Assert
        repositoryMock.Verify(r => r.GetAllGames());
        Assert.Contains(game1, games); 
        Assert.Contains(game2, games); 
    }
}