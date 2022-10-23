using System;
using System.Collections.Generic;
using Core.Domain;
using Moq;
using Portal.Controllers;
using Xunit;

namespace Core.DomainServices.Tests;

public class GameNightTest
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

    [Fact]
    public void GetParticipating_Returns_All_GameNights_In_Which_You_Participate()
    {
        // Arrange
        var kees = new User { Id = 1, Name = "Kees" };
        var jan = new User { Id = 2, Name = "Jan" };
        var gameNight1 = new GameNight { Id = 1, DateTime = new DateTime(2022, 3, 4), Players = new List<User>{ kees } };
        var gameNight2 = new GameNight { Id = 2, DateTime = new DateTime(2022, 5, 8), Players = new List<User>{ jan } };

        var repositoryMock = new Mock<IGameNightRepository>();
        repositoryMock
            .Setup(r => r.GetParticipating(kees))
            .Returns(new List<GameNight> { gameNight1 });

        var controller = new GameNightController(repositoryMock.Object, null, null, null);

        // Act
        var gameNights = controller.GetParticipating(kees);

        // Assert
        repositoryMock.Verify(r => r.GetParticipating(kees));
        Assert.Contains(gameNight1, gameNights);
        Assert.DoesNotContain(gameNight2, gameNights);
    }
}