using Core.Domain;
using Microsoft.AspNetCore.Identity;
using Moq;
using Portal.Controllers;
using Xunit;

namespace Core.DomainServices.Tests;

public class UserTest
{
    [Fact]
    public void GetUserByEmail()
    {
        // Arrange
        var user = new User { Id = 1, Email = "kees@gmail.com", Name = "Kees" };
        
        var repositoryMock = new Mock<IUserRepository>();
        repositoryMock
            .Setup(r => r.GetUserByEmail("kees@gmail.com"))
            .Returns(user);

        var controller = new AccountController(null, null, repositoryMock.Object);

        // Act
        var result = controller.GetUserByEmail("kees@gmail.com");

        // Assert
        repositoryMock.Verify(r => r.GetUserByEmail("kees@gmail.com"));
        Assert.Equal("Kees", result.Name);
    }

    // [Fact]
    //TODO:
    public void AddUser()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Name = "Jan",
            Email = "jan@gmail.com"
        };
        
        var repositoryMock = new Mock<IUserRepository>();
        repositoryMock
            .Setup(r => r.AddUser(user));
        
        var controller = new AccountController(null, null, repositoryMock.Object);

        // Act
        // var user = controller.GetUserByEmail("kees@gmail.com");

        // Assert
        repositoryMock.Verify(r => r.GetUserByEmail("kees@gmail.com"));
        Assert.Equal("Kees", user.Name);
    }
}