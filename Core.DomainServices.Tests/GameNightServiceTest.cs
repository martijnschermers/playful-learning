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
        var user = new User
        {
            Id = 1, Name = "Kees", BirthDate = new DateTime(1990, 10, 20), Allergies = new List<Allergy>()
        };
        var gameNight = new GameNight
        {
            Id = 1, DateTime = new DateTime(2022, 3, 4), Players = new List<User>(),
            MaxPlayers = 4, Foods = new List<Food>()
        };

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
        var user = new User
        {
            Id = 1, Name = "Kees", Type = UserType.Participant, BirthDate = new DateTime(2005, 10, 20)
        };
        var gameNight = new GameNight
        {
            Id = 1, DateTime = new DateTime(2022, 3, 4), Players = new List<User>(),
            IsOnlyForAdults = true, MaxPlayers = 4
        };

        var repositoryMock = new Mock<IGameNightRepository>();
        var gameNightServiceMock = new Mock<IGameNightService>();
        gameNightServiceMock
            .Setup(r => r.Participate(gameNight, user));

        var service = new GameNightService(repositoryMock.Object);

        // Act
        var result = service.Participate(gameNight, user);

        // Assert
        Assert.Equal(
            "Het is niet toegestaan om deel te nemen aan een spelavond voor volwassenen als iemand jonger dan 18 jaar!",
            result);
    }

    [Fact]
    public void Participate_For_Evening_Where_MaxPlayers_Is_Exceeded()
    {
        // Arrange
        var user = new User { Id = 1, Name = "Kees", BirthDate = new DateTime(2000, 10, 20) };
        var gameNight = new GameNight
        {
            Id = 1, DateTime = new DateTime(2022, 3, 4), Players = new List<User>(),
            IsOnlyForAdults = false, MaxPlayers = 0
        };

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

    [Fact]
    public void Participate_For_Evening_Where_Food_Does_Not_Include_Your_Wishes()
    {
        // Arrange
        var user = new User
        {
            Id = 1, Name = "Kees", BirthDate = new DateTime(2000, 10, 20),
            Allergies = new List<Allergy> { new Allergy { Name = AllergyEnum.Lactose } }
        };

        var gameNight = new GameNight
        {
            Id = 1, DateTime = new DateTime(2022, 3, 4), Players = new List<User>(),
            IsOnlyForAdults = false, MaxPlayers = 12,
            Foods = new List<Food> { new Food { Allergies = new List<Allergy> { new Allergy { Name = AllergyEnum.Gluten } } } }
        };

        var repositoryMock = new Mock<IGameNightRepository>();
        var gameNightServiceMock = new Mock<IGameNightService>();
        gameNightServiceMock
            .Setup(r => r.Participate(gameNight, user));

        var service = new GameNightService(repositoryMock.Object);

        // Act
        var result = service.Participate(gameNight, user);

        // Assert
        Assert.Equal("Uw allergieën of dieetwensen sluiten niet aan op deze spelavond!", result);
    }

    [Fact]
    public void Updating_GameNight_With_Participants_Returns_Error()
    {
        // Arrange
        var user = new User { Id = 1, Name = "Kees", BirthDate = new DateTime(2000, 10, 20) };
        var originalGameNight = new GameNight
        {
            Id = 1, DateTime = new DateTime(2022, 3, 4), Players = new List<User> { user },
            IsOnlyForAdults = false, MaxPlayers = 12, Organizer = user
        };

        var updatedGameNight = originalGameNight;
        updatedGameNight.MaxPlayers = 10;

        var repositoryMock = new Mock<IGameNightRepository>();
        repositoryMock
            .Setup(r => r.GetGameNightById(1))
            .Returns(originalGameNight);

        var gameNightServiceMock = new Mock<IGameNightService>();
        gameNightServiceMock
            .Setup(r => r.UpdateGameNight(originalGameNight.Id, updatedGameNight));

        var service = new GameNightService(repositoryMock.Object);

        // Act
        var result = service.UpdateGameNight(originalGameNight.Id, updatedGameNight);

        // Assert
        Assert.Equal("Het is niet toegestaan om de spelavond aan te passen, omdat er al deelnemers zijn.", result);
    }

    [Fact]
    public void Deleting_GameNight_With_Participants_Returns_Error()
    {
        // Arrange
        var user = new User { Id = 1, Name = "Kees", BirthDate = new DateTime(2000, 10, 20) };
        var originalGameNight = new GameNight
        {
            Id = 1, DateTime = new DateTime(2022, 3, 4), Players = new List<User> { user },
            IsOnlyForAdults = false, MaxPlayers = 12, Organizer = user
        };

        var repositoryMock = new Mock<IGameNightRepository>();
        repositoryMock
            .Setup(r => r.GetGameNightById(1))
            .Returns(originalGameNight);

        var gameNightServiceMock = new Mock<IGameNightService>();
        gameNightServiceMock
            .Setup(r => r.DeleteGameNight(originalGameNight.Id));

        var service = new GameNightService(repositoryMock.Object);

        // Act
        var result = service.DeleteGameNight(originalGameNight.Id);

        // Assert
        Assert.Equal("Het is niet toegestaan om de spelavond te verwijderen, omdat er al deelnemers zijn.", result);
    }

    [Fact]
    public void Adding_GameNight_With_Adult_Games_Makes_GameNight_For_Adults()
    {
        // Arrange
        var gameNight = new GameNight
        {
            Id = 1, DateTime = new DateTime(2022, 3, 4), Games = new List<Game> { new Game { IsOnlyForAdults = true } },
            IsOnlyForAdults = false, MaxPlayers = 12
        };

        var repositoryMock = new Mock<IGameNightRepository>();
        var gameNightServiceMock = new Mock<IGameNightService>();
        gameNightServiceMock
            .Setup(r => r.AddGameNight(gameNight));

        var service = new GameNightService(repositoryMock.Object);

        // Act
        service.AddGameNight(gameNight);

        // Assert
        Assert.True(gameNight.IsOnlyForAdults);
    }
}