using AutoFixture;
using FluentAssertions;
using TicketBooking.Classes;
using TicketBooking.Flights.Models;
using TicketBooking.Flights.Services;

namespace TicketBooking.Tests.Flights.Services;

public class FlightPrinterTests
{
    private readonly Flight _flight;
    private readonly Fixture _fixture;
    private readonly FlightPrinter _printer;

    public FlightPrinterTests()
    {
        _fixture = new Fixture();
        _printer = new();

        _flight = _fixture.Freeze<Flight>();
        _flight.Class = new Dictionary<Class, double> {
            { (Class)Enum.Parse(typeof(Class), "Economy"), 2500.5 }
        };

    }

    [Fact]
    public void GetFlightClassesAndPrices_Success()
    {
        // Arrange
        // Done

        // Act
        var classes = _printer.GetFlightClassesAndPrices(_flight);

        // Asset
        classes.Should().Contain($"Economy => $2500.5");
    }

    [Fact]
    public void GetFlightDetails_Success()
    {
        // Arrange
        // Done

        // Act
        var details = _printer.GetFlightDetails(_flight);

        // Asset
        details.Should().ContainAll(
            _flight.Id.ToString(),
            _flight.DepartureCountry.ToString(),
            _flight.DepartureCountry.ToString(),
            _flight.DepartureDate.ToString(),
            _flight.DepartureAirport.ToString(),
            _flight.ArrivalAirport.ToString(),
            $"Economy => $2500.5"
            );
    }

    [Fact]
    public void DisplayFlights_Success()
    {
        // Arrange
        var flightsList = _fixture.CreateMany<Flight>(5).ToList();

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act
        _printer.DisplayFlights(flightsList);
        var output = stringWriter.ToString();

        // Assert
        // one line before displaying and one after
        output.Split('\n').Should().HaveCount(flightsList.Count + 2);
    }
}