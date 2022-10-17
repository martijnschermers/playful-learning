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
            Email = "jan@gmail.com", BirthDate = new DateTime(2000, 3, 20)
        };

        // Act
        var age = user.GetAge();
        
        // Assert
        Assert.Equal(22, age);
    }
}