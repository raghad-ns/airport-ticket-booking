using AutoFixture;
using Moq;
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
    public void Login_ShouldCallRepositoryMethod()
    {
        // Arrange
        var email = _fixture.Create<string>();
        var password = _fixture.Create<string>();

        // Act
        _userService.Login(email, password);

        // Assert
        _userRepositoryMock.Verify(repo => repo.Login(email, password), "UserRepository.Login should be called exactly once");
    }

    [Fact]
    public void GetUsers_ShouldCallRepositoryMethod()
    {
        // Arrange
        // Done

        // Act
        _userService.GetUsers();

        // Assert
        _userRepositoryMock.Verify(repo => repo.GetUsers(), "UserRepository.Login should be called exactly once");
    }
}