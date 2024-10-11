using AutoFixture;
using FluentAssertions;
using Moq;
using TicketBooking.Users;
using TicketBooking.Users.Repository;
using TicketBooking.Users.Services;

namespace TicketBooking.Tests.Users.Services;

public class UserServicesTests
{
    private readonly UserServices _userService;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Fixture _fixture;

    public UserServicesTests()
    {
        _fixture = new Fixture();
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserServices(_userRepositoryMock.Object);
    }

    [Fact]
    public void Login_ShouldReturnUserObject()
    {
        // Arrange
        var user = _fixture.Freeze<UserModel>();
        _userRepositoryMock.Setup(repo => repo.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(user);

        // Act
        var users = _userRepositoryMock.Object.GetUsers();
        var loggedInUser = _userService.Login(user.Email, user.Password);

        // Assert
        loggedInUser.Should().BeEquivalentTo(user);
    }

    [Fact]
    public void GetUsers_ShouldRetyrnUsersList()
    {
        // Arrange
        var users = _fixture.CreateMany<UserModel>().ToList();
        _userRepositoryMock.Setup(repo => repo.GetUsers()).Returns(users);

        // Act
        var returnedUsers = _userService.GetUsers();

        // Assert
        returnedUsers.Should().BeEquivalentTo(users);
    }
}