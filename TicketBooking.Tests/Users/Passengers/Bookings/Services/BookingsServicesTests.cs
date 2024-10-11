using AutoFixture;
using FluentAssertions;
using Moq;
using TicketBooking.Classes;
using TicketBooking.Flights.Models;
using TicketBooking.Users.Passengers.Bookings;

namespace TicketBooking.Tests.Users.Passengers.Bookings.Services;

public class BookingsServicesTests
{
    private readonly Mock<IBookingsRepository> _bookingsRepositoryMock;
    private readonly BookingsService _bookingsService;
    private readonly Fixture _fixture;
    private readonly List<BookingsModel> _bookings;

    public BookingsServicesTests()
    {
        _bookingsRepositoryMock = new Mock<IBookingsRepository>();
        _fixture = new Fixture();
        _bookings = _fixture.Create<List<BookingsModel>>();
        _bookingsService = new(_bookings, _bookingsRepositoryMock.Object);
    }

    [Fact]
    public void AddBooking_ShouldAddBookingSuccessfully()
    {
        // Arrange
        int bookingsListLength = _bookings.Count;
        var flight = _fixture.Create<Flight>();
        var flightClass = _fixture.Create<Class>();
        var userId = _fixture.Create<int>();

        // Act
        _bookingsService.AddBooking(flight, flightClass, userId);

        // Assert
        _bookings.Should().HaveCount(bookingsListLength + 1);
        _bookingsRepositoryMock.Verify(repo => repo.AddBookingToFile(It.IsAny<BookingsModel>()));
    }

    [Fact]
    public void GetBookingsTest_ShouldReturnListOfBookingModel()
    {
        // Arrange
        // Done

        // Act
        var bookingsList = _bookingsService.GetBookings();

        // Assert
        bookingsList.Should().BeAssignableTo<List<BookingsModel>>();
    }
}