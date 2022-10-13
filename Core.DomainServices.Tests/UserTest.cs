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
        var repositoryMock = new Mock<IUserRepository>();
        var signInManagerMock = new Mock<SignInManager<IdentityUser>>();
        var userManagerMock = new Mock<UserManager<IdentityUser>>(); 
        repositoryMock
            .Setup(r => r.GetUserByEmail("kees@gmail.com"))
            .Returns(new User { Id = 1, Email = "kees@gmail.com", Name = "Kees" });

        var controller = new AccountController(userManagerMock.Object, signInManagerMock.Object, repositoryMock.Object);

        // Act
        var user = controller.GetUserByEmail("kees@gmail.com");

        // Assert
        repositoryMock.Verify(r => r.GetUserByEmail("kees@gmail.com"));
        Assert.Equal("Kees", user.Name);
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
        var signInManagerMock = new Mock<SignInManager<IdentityUser>>();
        var userManagerMock = new Mock<UserManager<IdentityUser>>(); 
        repositoryMock
            .Setup(r => r.AddUser(user));
        
        var controller = new AccountController(userManagerMock.Object, signInManagerMock.Object, repositoryMock.Object);

        // Act
        // var user = controller.GetUserByEmail("kees@gmail.com");

        // Assert
        repositoryMock.Verify(r => r.GetUserByEmail("kees@gmail.com"));
        Assert.Equal("Kees", user.Name);
    }
}