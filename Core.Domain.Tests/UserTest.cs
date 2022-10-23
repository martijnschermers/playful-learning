using System;
using Xunit;

namespace Core.Domain.Tests;

public class UserTest
{
    [Fact]
    public void GetAge_Returns_Valid_Age()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Name = "Jan",
            Email = "jan@gmail.com", 
            BirthDate = new DateTime(2000, 3, 20)
        };

        // Act
        var age = user.GetAge(null);
        
        // Assert
        Assert.Equal(22, age);
    }

    [Fact]
    public void BirthDate_In_The_Future_Throws_InvalidOperationException()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Name = "Jan",
            Email = "jan@gmail.com", 
        };

        // Act
        var act = Assert.Throws<InvalidOperationException>(() => user.BirthDate = new DateTime(DateTime.Now.AddYears(1).Year, 3, 20));
        
        // Assert
        Assert.Equal("De geboortedatum mag niet in de toekomst liggen!", act.Message);
    }

    [Fact]
    public void Age_Lower_Than_18_For_Organiser_Throws_InvalidOperationException()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Name = "Jan",
            Email = "jan@gmail.com", 
            Type = UserType.Organizer
        };

        // Act
        var act = Assert.Throws<InvalidOperationException>(() => user.BirthDate = new DateTime(2009, 3, 20));
        
        // Assert
        Assert.Equal("Je moet 18 jaar oud zijn om een organisator te zijn!", act.Message);
    }
}