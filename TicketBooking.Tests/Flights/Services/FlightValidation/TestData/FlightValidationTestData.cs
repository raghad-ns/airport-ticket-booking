using TicketBooking.Airports;
using TicketBooking.Countries;
using TicketBooking.FileProcessor.Deserializer.Flight;

namespace TicketBooking.Tests.Flights.Services.FlightValidation.TestData;

public class FlightValidationTestData
{
    public static IEnumerable<object[]> TestData
    {
        get
        {
            yield return new object[] {
                new FlightSerialization(
                    -5,
                    Country.Palestine.ToString(),
                    Country.Germany.ToString(),
                    "AP105",
                    new Dictionary<string, double> { { "Economy", 2500.5 } },
                    DateTime.Now.AddDays(1),
                    Airport.QueenAliaInternationalAirport.ToString(),
                    Airport.QueenAliaInternationalAirport.ToString()
                ),
                "Id is invalid are duplicated, should be unique and greater than 0"
            };

            yield return new object[] {
                new FlightSerialization(
                    1,
                    "None",
                    Country.Germany.ToString(),
                    "AP105",
                    new Dictionary<string, double> { { "Economy", 2500.5 } },
                    DateTime.Now.AddDays(1),
                    Airport.QueenAliaInternationalAirport.ToString(),
                    Airport.QueenAliaInternationalAirport.ToString()
                ),
                "Departure country is not found"
            };

            yield return new object[] {
                new FlightSerialization(
                    1,
                    Country.Palestine.ToString(),
                    "None",
                    "AP105",
                    new Dictionary<string, double> { { "Economy", 2500.5 } },
                    DateTime.Now.AddDays(1),
                    Airport.QueenAliaInternationalAirport.ToString(),
                    Airport.QueenAliaInternationalAirport.ToString()
                ),
                "Destination country is not found"
            };

            yield return new object[] {
                new FlightSerialization(
                    1,
                    Country.Palestine.ToString(),
                    Country.Germany.ToString(),
                    "AP105",
                    new Dictionary<string, double> { { "Luxury", 2500.5 } },
                    DateTime.Now.AddDays(1),
                    Airport.QueenAliaInternationalAirport.ToString(),
                    Airport.QueenAliaInternationalAirport.ToString()
                ),
                "Valid classes are: FirstClass, Business, Economy. Prices should be positive values too"
            };

            yield return new object[] {
                new FlightSerialization(
                    1,
                    Country.Palestine.ToString(),
                    Country.Germany.ToString(),
                    "AP105",
                    new Dictionary<string, double> { { "Economy", 2500.5 } },
                    DateTime.Now.AddDays(-1),
                    Airport.QueenAliaInternationalAirport.ToString(),
                    Airport.QueenAliaInternationalAirport.ToString()
                ),
                "Invalid departure time, should be future date"
            };

            yield return new object[] {
                new FlightSerialization(
                    1,
                    Country.Palestine.ToString(),
                    Country.Germany.ToString(),
                    "AP105",
                    new Dictionary<string, double> { { "Economy", 2500.5 } },
                    DateTime.Now.AddDays(1),
                    "None",
                    Airport.QueenAliaInternationalAirport.ToString()
                ),
                "Departure airport is not found"
            };

            yield return new object[] {
                new FlightSerialization(
                    1,
                    Country.Palestine.ToString(),
                    Country.Germany.ToString(),
                    "AP105",
                    new Dictionary<string, double> { { "Economy", 2500.5 } },
                    DateTime.Now.AddDays(1),
                    Airport.QueenAliaInternationalAirport.ToString(),
                    "None"
                ),
                "Arrival airport is not found"
            };
        }
    }
}