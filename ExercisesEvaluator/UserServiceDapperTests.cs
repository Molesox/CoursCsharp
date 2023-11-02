namespace ExercisesEvaluator;

using System.Security.AccessControl;
using Todapp.Data.Context;
using Todapp.Data.Contracts;
using Todapp.Data.Services;
using Todapp.Models;

public class UserServiceDapperTests
{

    private readonly IUserService _userService;
    private int testUserID;

    public UserServiceDapperTests()
    {
        var context = new SqlContext("Server=WEBDEV03\\sqldev2019;User Id=sa;Password=syslog$1;Database=TodappDB;TrustServerCertificate=false;Encrypt=false;");

        _userService = new UserServiceDapper(context);
    }


    [Fact]
    public async Task test01()
    {
        // Arrange
        var user = new User { UserName = "TestUser" };

        // Act
        var result = await _userService.AddUserAsync(user);

        // Assert
        Assert.NotEqual(0, result.UserId);
        Assert.Equal("TestUser", result.UserName);

        testUserID = result.UserId;
    }

    [Fact]
    public async Task test02()
    {
        // Arrange
        var user = new User { UserId = testUserID };

        // Act
        var result = await _userService.DeleteUserAsync(user);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task test03()
    {
        // Act
        var result = await _userService.GetUsersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);

    }

    [Fact]
    public async Task test04()
    {
        // Arrange
        var user = new User { UserId = testUserID, UserName = "UpdatedUser" };


        // Act
        var result = await _userService.UpdateUserAsync(user);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testUserID, result.UserId);
        Assert.Equal("UpdatedUser", result.UserName);
    }

    [Fact]
    public async Task test05()
    {
        // Arrange
        var user = new User { UserId = -1, UserName = "NotUpdatedUser" };


        // Act
        var result = await _userService.UpdateUserAsync(user);

        // Assert
        Assert.Null(result);
    }
}