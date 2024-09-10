using System.Text;
using TicketBooking.Airports;
using TicketBooking.Classes;
using TicketBooking.Countries;
using TicketBooking.FileProcessor.Deserializer.Flight;
using TicketBooking.Flights.Models;

namespace TicketBooking.Flights.Services;

public class FlightValidator
{
    private List<Flight> _flights { get; init; }

    public FlightValidator(List<Flight> existedFlights)
    {
        _flights = existedFlights;
    }

    public string ValidateFlightProperties(FlightSerialization flight)
    {
        var (id,
        departureCountry,
        destinationCountry,
        flightNo,
        flightClassesDict,
        departureTime,
        departureAirport,
        arrivalAirport) = flight;

        StringBuilder sb = new StringBuilder();

        if (!validateId(id)) sb.AppendLine("Id is invalid are duplicated, should be unique and greater than 0");

        if (!ValidateCountry(departureCountry)) sb.AppendLine("Departure country is not found");

        if (!ValidateCountry(destinationCountry)) sb.AppendLine("Destination country is not found");

        if (!ValidateAirport(departureAirport)) sb.AppendLine("Departure airport is not found");

        if (!ValidateAirport(arrivalAirport)) sb.AppendLine("Arrival airport is not found");

        if (!ValidateDepartureTime(departureTime)) sb.AppendLine("Invalid departure time, should be future date");

        if (!ValidateFlightClass(flightClassesDict)) sb.AppendLine("Valid classes are: FirstClass, Business, Economy. Prices should be positive values too");

        return sb.ToString();
    }

    public bool validateId(int id)
    {
        try { return IsValidId(id) && !IsDuplicatedId(id); }
        catch { return false; }
    }

    private bool IsValidId(int id) => id > 0;

    private bool IsDuplicatedId(int id) => _flights.Count(flight => flight.Id == id) > 0;

    public bool ValidateCountry(string country) => Enum.TryParse<Country>(country, out _);

    // Same functionality could be achieved this way too
    //public bool ValidateCountry(string country)
    //{
    //    try
    //    {
    //        return
    //            new List<string>(Enum.GetNames(typeof(Country)))
    //            .SingleOrDefault(existedCountry => existedCountry.Equals(country))
    //            .Any();
    //    }
    //    catch { return false; }
    //}

    public bool ValidateAirport(string airport) => Enum.TryParse<Airport>(airport, out _);
    public bool ValidateDepartureTime(DateTime dapartureTime)
    {
        return dapartureTime >= DateTime.Now;
    }

    public bool ValidateFlightClass(Dictionary<string, double> classes)
    {
        bool valid = true;

        try
        {
            foreach (var flightClass in classes)
            {
                // In fact, there is no need for to next line, since classes specified in the code, not from user's input
                if (!Enum.TryParse(flightClass.Key, out Class enumValue)) valid = false;
                if (flightClass.Value <= 0) valid = false;
            }

        }
        catch { valid = false; }

        return valid;
    }
}