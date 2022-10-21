using Core.Domain;
using Moq;
using Portal.Controllers;
using Xunit;

namespace Core.DomainServices.Tests;

public class UserTest
{
    [Fact]
    public void GetUserByEmail_Returns_The_Correct_User_By_Email()
    {
        // Arrange
        var user = new User { Id = 1, Email = "kees@gmail.com", Name = "Kees" };
        
        var repositoryMock = new Mock<IUserRepository>();
        repositoryMock
            .Setup(r => r.GetUserByEmail("kees@gmail.com"))
            .Returns(user);

        var controller = new AccountController(repositoryMock.Object, null, null, null);

        // Act
        var result = controller.GetUserByEmail("kees@gmail.com");

        // Assert
        repositoryMock.Verify(r => r.GetUserByEmail("kees@gmail.com"));
        Assert.Equal("Kees", result.Name);
    }

    // [Fact]
    //TODO:
    public void AddUser_Adds_A_User()
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
        
        var controller = new AccountController(repositoryMock.Object, null, null, null);

        // Act
        // var user = controller.GetUserByEmail("kees@gmail.com");

        // Assert
        repositoryMock.Verify(r => r.GetUserByEmail("kees@gmail.com"));
        Assert.Equal("Kees", user.Name);
    }
}