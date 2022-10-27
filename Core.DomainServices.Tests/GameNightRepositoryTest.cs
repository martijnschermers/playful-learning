using System;
using System.Collections.Generic;
using Core.Domain;
using Core.DomainServices.Repositories.Interface;
using Moq;
using Portal.Controllers;
using Xunit;

namespace Core.DomainServices.Tests;

public class GameNightRepositoryTest
{
    [Fact]
    public void GetGameNightById_Returns_Correct_GameNight()
    {
        // Arrange
        var repositoryMock = new Mock<IGameNightRepository>();
        repositoryMock
            .Setup(r => r.GetGameNightById(1))
            .Returns(new GameNight { Id = 1, DateTime = new DateTime(2022, 4, 2) });

        var controller = new GameNightController(repositoryMock.Object, null, null, null);

        // Act
        var gameNight = controller.GetGameNightById(1)!;

        // Assert
        repositoryMock.Verify(r => r.GetGameNightById(1));
        Assert.Equal(new DateTime(2022, 4, 2), gameNight.DateTime);
    }

    [Fact]
    public void GetAllGameNights_Returns_All_GameNights()
    {
        // Arrange
        var gameNight1 = new GameNight { Id = 1, DateTime = new DateTime(2022, 3, 4) };
        var gameNight2 = new GameNight { Id = 2, DateTime = new DateTime(2022, 5, 8) };

        var repositoryMock = new Mock<IGameNightRepository>();
        repositoryMock
            .Setup(r => r.GetAllGameNights())
            .Returns(new List<GameNight> { gameNight1, gameNight2 });

        var controller = new GameNightController(repositoryMock.Object, null, null, null);

        // Act
        var games = controller.GetAllGameNights();

        // Assert
        repositoryMock.Verify(r => r.GetAllGameNights());
        Assert.Contains(gameNight1, games);
        Assert.Contains(gameNight2, games);
    }
}