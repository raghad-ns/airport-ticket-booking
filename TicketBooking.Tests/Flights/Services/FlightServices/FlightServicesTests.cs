using AutoFixture;
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
    public void LoadFlights_ShouldCallRepositoryMethod()
    {
        // Arrange
        var path = "C:\\Users\\M.T\\Desktop\\projects\\foothill\\practice-projects\\AirportTicketBooking\\TicketBooking\\Flights\\Repository\\Flights.csv";

        // Act
        _flightService.LoadFlights(path);

        // Assert
        _flightRepositoryMock.Verify(repo => repo.LoadFlights(It.IsAny<string>()), "FlightRepository.LoadFlights should be called exactly once");
    }

    [Fact]
    public void FilterFlights_ShouldCallRepositoryMethod()
    {
        // Arrange
        var priceFrom = _fixture.Create<double>();
        var priceTo = _fixture.Create<double>();
        var departureCountry = _fixture.Create<Country>().ToString();
        var destinationCountry = _fixture.Create<Country>().ToString();
        var departureAirport = _fixture.Create<Airport>().ToString();
        var arrivalAirport = _fixture.Create<Airport>().ToString();
        var flightClass = _fixture.Create<Class>();

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
        _flightRepositoryMock
            .Verify(repo =>
            repo.FilterFlights(
                priceFrom,
                priceTo,
                departureCountry,
                destinationCountry,
                departureAirport,
                arrivalAirport,
                flightClass)
            , "FlightRepository.FilterFlights should be called exactly once");
    }

    [Fact]
    public void GetFlights_ShouldCallRepositoryMethod()
    {
        // Arrange
        // Done

        // Act
        _flightService.GetFlights();

        // Assert
        _flightRepositoryMock.Verify(repo => repo.GetFlights(), "FlightRepository.GetFlights should be called exactly once");
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
    public void GetFlightClassesAndPricesForDisplay_ShouldCallPrinterMethod()
    {
        // Arrange
        // Done

        // Act
        _flightService.GetFlightClassesAndPricesForDisplay(_fixture.Create<Flight>());

        // Assert
        _flightPrinterMock.Verify(printer => printer.GetFlightClassesAndPrices(It.IsAny<Flight>()), "FlightPrinter.GetFlightClassesAndPricesForDisplay should be called exactly once");
    }

    [Fact]
    public void GetFlightDetailsForDisplay_ShouldCallPrinterMethod()
    {
        // Arrange
        // Done

        // Act
        _flightService.GetFlightDetailsForDisplay(_fixture.Create<Flight>());

        // Assert
        _flightPrinterMock.Verify(printer => printer.GetFlightDetails(It.IsAny<Flight>()), "FlightPrinter.GetFlightDetailsForDisplay should be called exactly once");
    }
}