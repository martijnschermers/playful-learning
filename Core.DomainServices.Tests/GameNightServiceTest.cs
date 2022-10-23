using System;
using System.Collections.Generic;
using Core.Domain;
using Moq;
using Xunit;

namespace Core.DomainServices.Tests;

public class GameNightServiceTest
{
    [Fact]
    public void Participate_Successful()
    {
        // Arrange
        var user = new User { Id = 1, Name = "Kees", BirthDate = new DateTime(1990, 10, 20) };
        var gameNight = new GameNight { Id = 1, DateTime = new DateTime(2022, 3, 4), Players = new List<User>(), IsOnlyForAdults = true, MaxPlayers = 4 };

        var repositoryMock = new Mock<IGameNightRepository>();
        var gameNightServiceMock = new Mock<IGameNightService>();
        gameNightServiceMock
            .Setup(r => r.Participate(gameNight, user));

        var service = new GameNightService(repositoryMock.Object);

        // Act
        var result = service.Participate(gameNight, user);

        // Assert
        Assert.Equal("", result);
    }
    
    [Fact]
    public void Participate_For_Adult_Evening_While_Under_18()
    {
        // Arrange
        var user = new User { Id = 1, Name = "Kees", BirthDate = new DateTime(2009, 10, 20) };
        var gameNight = new GameNight { Id = 1, DateTime = new DateTime(2022, 3, 4), Players = new List<User>(), IsOnlyForAdults = true, MaxPlayers = 4 };

        var repositoryMock = new Mock<IGameNightRepository>();
        var gameNightServiceMock = new Mock<IGameNightService>();
        gameNightServiceMock
            .Setup(r => r.Participate(gameNight, user));

        var service = new GameNightService(repositoryMock.Object);

        // Act
        var result = service.Participate(gameNight, user);

        // Assert
        Assert.Equal("Het is niet toegestaan om deel te nemen aan een spelavond voor volwassenen als iemand jonger dan 18 jaar!", result);
    }
    
    [Fact]
    public void Participate_For_Evening_Where_MaxPlayers_Is_Exceeded()
    {
        // Arrange
        var user = new User { Id = 1, Name = "Kees", BirthDate = new DateTime(2009, 10, 20) };
        var gameNight = new GameNight { Id = 1, DateTime = new DateTime(2022, 3, 4), Players = new List<User>(), IsOnlyForAdults = false, MaxPlayers = 0 };

        var repositoryMock = new Mock<IGameNightRepository>();
        var gameNightServiceMock = new Mock<IGameNightService>();
        gameNightServiceMock
            .Setup(r => r.Participate(gameNight, user));

        var service = new GameNightService(repositoryMock.Object);

        // Act
        var result = service.Participate(gameNight, user);

        // Assert
        Assert.Equal("Het is niet mogelijk om in te schrijven, omdat de spelavond vol is!", result);
    }
}