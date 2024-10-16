using AutoFixture;
using FluentAssertions;
using Moq;
using TicketBooking.Airports;
using TicketBooking.Classes;
using TicketBooking.Countries;
using TicketBooking.Flights.Models;
using TicketBooking.Flights.Repository;

namespace TicketBooking.Flights.Services;

public class FlightServicesTests
{
    private readonly FlightServices _flightService;
    private readonly Mock<IFlightRepository> _flightRepositoryMock;
    private readonly Mock<List<Flight>> _flightsMock;
    private readonly Mock<IFlightPrinter> _flightPrinterMock;
    private readonly Fixture _fixture;

    public FlightServicesTests()
    {
        _flightRepositoryMock = new Mock<IFlightRepository>();
        _flightsMock = new Mock<List<Flight>>();
        _flightPrinterMock = new Mock<IFlightPrinter>();
        _flightService = new(_flightsMock.Object, _flightRepositoryMock.Object, _flightPrinterMock.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public void LoadFlights_ShouldLoadFlightsFromCSVFileAndReturnFlightsList()
    {
        // Arrange
        var path = "C:\\Users\\M.T\\Desktop\\projects\\foothill\\practice-projects\\AirportTicketBooking\\TicketBooking\\Flights\\Repository\\Flights.csv";
        var flightsList = _fixture.CreateMany<Flight>().ToList();

        _flightRepositoryMock
            .Setup(repo =>repo.LoadFlights(It.IsAny<string>()))
            .Returns(flightsList);

        // Act
        var flights = _flightService.LoadFlights(path);

        // Assert
        flights.Should().BeEquivalentTo(flightsList);
    }

    [Fact]
    public void FilterFlights_ShouldReturnFlights_FiltersMatch()
    {
        // Arrange
        var priceFrom = _fixture.Create<double>();
        var priceTo = _fixture.Create<double>();
        var departureCountry = _fixture.Create<Country>().ToString();
        var destinationCountry = _fixture.Create<Country>().ToString();
        var departureAirport = _fixture.Create<Airport>().ToString();
        var arrivalAirport = _fixture.Create<Airport>().ToString();
        var flightClass = _fixture.Create<Class>();
        var flightsList = _fixture.CreateMany<Flight>().ToList();
        
        _flightRepositoryMock
            .Setup(repo =>
            repo.FilterFlights(
                priceFrom,
                priceTo,
                departureCountry,
                destinationCountry,
                departureAirport,
                arrivalAirport,
                flightClass))
            .Returns(flightsList);

        // Act
        var filteredFlights = _flightService.FilterFlights(
                priceFrom,
                priceTo,
                departureCountry,
                destinationCountry,
                departureAirport,
                arrivalAirport,
                flightClass
            );

        // Assert
        filteredFlights.Should().BeEquivalentTo(flightsList);
    }

    [Fact]
    public void FilterFlights_ShouldReturnEmptyList_FiltersMismatch()
    {
        // Arrange
        var priceFrom = _fixture.Create<double>();
        var priceTo = _fixture.Create<double>();
        var departureCountry = _fixture.Create<Country>().ToString();
        var destinationCountry = _fixture.Create<Country>().ToString();
        var departureAirport = _fixture.Create<Airport>().ToString();
        var arrivalAirport = _fixture.Create<Airport>().ToString();
        var flightClass = _fixture.Create<Class>();

        _flightRepositoryMock
            .Setup(repo =>
            repo.FilterFlights(
                priceFrom,
                priceTo,
                departureCountry,
                destinationCountry,
                departureAirport,
                arrivalAirport,
                flightClass))
            .Returns(new List<Flight>());

        // Act
        var filteredFlights = _flightService.FilterFlights(
                priceFrom,
                priceTo,
                departureCountry,
                destinationCountry,
                departureAirport,
                arrivalAirport,
                flightClass
            );

        // Assert
        filteredFlights.Should().HaveCount(0);
    }

    [Fact]
    public void GetFlights_ShouldReturnFlightsList()
    {
        // Arrange
        var flights = _fixture.CreateMany<Flight>().ToList();
        _flightRepositoryMock.Setup(repo => repo.GetFlights()).Returns(flights);

        // Act
        var returnedFlights = _flightService.GetFlights();

        // Assert
        returnedFlights.Should().BeEquivalentTo(flights);
    }

    [Fact]
    public void DisplayFlights_ShouldCallPrinterMethod()
    {
        // Arrange
        // Done

        // Act
        _flightService.DisplayFlights();

        // Assert
        _flightPrinterMock.Verify(printer => printer.DisplayFlights(It.IsAny<List<Flight>>()), "FlightPrinter.DisplayFlights should be called exactly once");
    }

    [Fact]
    public void GetFlightClassesAndPricesForDisplay_ShouldReturnFlightClassesAsString()
    {
        // Arrange
        var flightClass = _fixture.Create<string>();
        _flightPrinterMock.Setup(printer => printer.GetFlightClassesAndPrices(It.IsAny<Flight>())).Returns(flightClass);

        // Act
        var returnedClass = _flightService.GetFlightClassesAndPricesForDisplay(_fixture.Create<Flight>());

        // Assert
        returnedClass.Should().Be(flightClass);
    }

    [Fact]
    public void GetFlightDetailsForDisplay_ShouldReturnFlightDetailsAsString()
    {
        // Arrange
        var flight = _fixture.Create<string>();
        _flightPrinterMock.Setup(printer => printer.GetFlightDetails(It.IsAny<Flight>())).Returns(flight);

        // Act
        var flightDetails = _flightService.GetFlightDetailsForDisplay(_fixture.Create<Flight>());

        // Assert
        flightDetails.Should().Be(flight);
    }
}