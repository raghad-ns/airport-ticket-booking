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
    [InlineData(-5, false)]
    public void ValidateIdTest(int id, bool expectedResult)
    {
        // Arrange
        // Done

        // Act
        bool idValidation = _validator.validateId(id);

        // Assert
        idValidation.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("QueenAliaInternationalAirport", true)]
    [InlineData("CairoAirport", false)]
    public void ValidateAirportTest(string airport, bool expectedResult)
    {
        // Arrange
        // Done

        // Act
        bool airportValidation = _validator.ValidateAirport(airport);

        // Assert
        airportValidation.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("Palestine", true)]
    [InlineData("London", false)]
    public void ValidateCountryTest(string country, bool expectedResult)
    {
        // Arrange
        // Done

        // Act
        bool countryValidation = _validator.ValidateCountry(country);

        // Assert
        countryValidation.Should().Be(expectedResult);
    }

    [Theory]
    [MemberData("TestData", MemberType = typeof(FlightClassValidationTestData))]
    public void ValidateFlightClassTest(Dictionary<string, double> flightClass, bool expectedResult)
    {
        // Arrange
        // Done

        // Act
        bool flightClassValidation = _validator.ValidateFlightClass(flightClass);

        // Assert
        flightClassValidation.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(3, true)]
    [InlineData(-5, false)]
    public void ValidateDepartureDateTest(int days, bool expectedResult)
    {
        // Arrange
        // Done

        // Act
        bool departureTimeValidation = _validator.ValidateDepartureTime(DateTime.Now.AddDays(days));

        // Assert
        departureTimeValidation.Should().Be(expectedResult);
    }

    [Fact]
    public void IdDuplicationTest()
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
    public void ValidateAllProperties_InvalidReturns(FlightSerialization flightDetails, string expectedResult)
    {
        // Arrange
        // Done

        // Act
        string result = _validator.ValidateFlightProperties(flightDetails);

        // Assert
        result.Should().Contain(expectedResult);
    }

    [Fact]
    public void ValidateAllProperties_SuccessStatus()
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