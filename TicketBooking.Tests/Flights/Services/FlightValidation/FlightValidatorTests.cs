using AutoFixture;
using FluentAssertions;
using Moq;
using TicketBooking.Flights.Models;
using TicketBooking.Flights.Services;
using TicketBooking.FileProcessor.Deserializer.Flight;
using TicketBooking.Tests.Flights.Services.FlightValidation.TestData;
using TicketBooking.Airports;
using TicketBooking.Countries;

namespace TicketBooking.Tests.Flights.Services.FlightValidation;

public class FlightValidatorTests
{
    private readonly Fixture _fixture;
    private FlightValidator _validator;
    private readonly Mock<List<Flight>> _flightsMock;

    public FlightValidatorTests()
    {
        _flightsMock = new();
        _validator = new(_flightsMock.Object);
        _fixture = new Fixture();
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(3089799, true)]
    [InlineData(4234, true)]
    public void ValidateId_ReturnTrue(int id, bool expectedResult)
    {
        // Arrange
        // Done

        // Act
        bool idValidation = _validator.validateId(id);

        // Assert
        idValidation.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(-5, false)]
    [InlineData(-43248, false)]
    [InlineData(-747382, false)]
    public void ValidateId_ReturnFalse(int id, bool expectedResult)
    {
        // Arrange
        // Done

        // Act
        bool idValidation = _validator.validateId(id);

        // Assert
        idValidation.Should().Be(expectedResult);
    }

    [Fact]
    public void ValidateAirport_ReturnTrue_ExistedAirport()
    {
        // Arrange
        string airport = "QueenAliaInternationalAirport";

        // Act
        bool airportValidation = _validator.ValidateAirport(airport);

        // Assert
        airportValidation.Should().BeTrue();
    }

    [Fact]
    public void ValidateAirport_ReturnFalse_NotExistedAirport()
    {
        // Arrange
        string airport = "CairoAirport";

        // Act
        bool airportValidation = _validator.ValidateAirport(airport);

        // Assert
        airportValidation.Should().BeFalse();
    }

    [Fact]
    public void ValidateCountry_ReturnTrue_ExistedCountry()
    {
        // Arrange
        var country = "Palestine";

        // Act
        bool countryValidation = _validator.ValidateCountry(country);

        // Assert
        countryValidation.Should().BeTrue();
    }

    [Fact]
    public void ValidateCountryReturnFalse_NotExistedCountry()
    {
        // Arrange
        var country = "London";

        // Act
        bool countryValidation = _validator.ValidateCountry(country);

        // Assert
        countryValidation.Should().BeFalse();
    }

    [Fact]
    public void ValidateFlightClass_ReturnTrue_ExistedClass()
    {
        // Arrange
        var flightClass = new Dictionary<string, double> { { "Economy", 2500.5 } };

        // Act
        bool flightClassValidation = _validator.ValidateFlightClass(flightClass);

        // Assert
        flightClassValidation.Should().BeTrue();
    }

    [Fact]
    public void ValidateFlightClass_ReturnFalse_NotExistedClass()
    {
        // Arrange
        var flightClass = new Dictionary<string, double> { { "Luxury", 7000 } };

        // Act
        bool flightClassValidation = _validator.ValidateFlightClass(flightClass);

        // Assert
        flightClassValidation.Should().BeFalse();
    }

    [Theory]
    [InlineData(3)]
    [InlineData(40)]
    [InlineData(23)]
    public void ValidateDepartureDate_ReturnTrue_FutureDate(int days)
    {
        // Arrange
        var departureDate = DateTime.Now.AddDays(days);

        // Act
        bool departureTimeValidation = _validator.ValidateDepartureTime(departureDate);

        // Assert
        departureTimeValidation.Should().BeTrue();
    }

    [Theory]
    [InlineData(-3)]
    [InlineData(-40)]
    [InlineData(-23)]
    public void ValidateDepartureDate_ReturnFalse_PastDate(int days)
    {
        // Arrange
        var departureDate = DateTime.Now.AddDays(days);

        // Act
        bool departureTimeValidation = _validator.ValidateDepartureTime(departureDate);

        // Assert
        departureTimeValidation.Should().BeFalse();
    }

    [Fact]
    public void IdDuplication_ReturnFalse_IdAlreadyExisted()
    {
        // Arrange
        var flight = _fixture.Freeze<Flight>();
        _flightsMock.Object.Add(flight);
        _validator = new(_flightsMock.Object);

        // Act
        var result = _validator.validateId(flight.Id);

        //Assert
        result.Should().BeFalse();
    }

    [Theory]
    [MemberData("TestData", MemberType = typeof(FlightValidationTestData))]
    public void ValidateAllProperties_ReturnsValidationErrorMesage(FlightSerialization flightDetails, string ValidationErrorMessage)
    {
        // Arrange
        // Done

        // Act
        string result = _validator.ValidateFlightProperties(flightDetails);

        // Assert
        result.Should().Contain(ValidationErrorMessage);
    }

    [Fact]
    public void ValidateAllProperties_ReturnsEmptyString_AllPropertiesAreValid()
    {
        // Arrange
        var flightDetails = new FlightSerialization(
                    1,
                    Country.Palestine.ToString(),
                    Country.Germany.ToString(),
                    "AP105",
                    new Dictionary<string, double> { { "Economy", 2500.5 } },
                    DateTime.Now.AddDays(1),
                    Airport.QueenAliaInternationalAirport.ToString(),
                    Airport.QueenAliaInternationalAirport.ToString()
                );

        // Act
        string result = _validator.ValidateFlightProperties(flightDetails);

        // Assert
        result.Should().BeEmpty();
    }
}