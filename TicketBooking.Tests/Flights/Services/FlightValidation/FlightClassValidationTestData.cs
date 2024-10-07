namespace TicketBooking.Tests.Flights.Services.FlightValidation;

public class FlightClassValidationTestData
{
    public static IEnumerable<object[]> TestData
    {
        get
        {
            yield return new object[] { new Dictionary<string, double> { { "Economy", 2500.5 } }, true };
            yield return new object[] { new Dictionary<string, double> { { "Luxury", 5000 } }, false };
        }
    }
}