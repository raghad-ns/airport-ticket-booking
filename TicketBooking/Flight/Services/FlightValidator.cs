using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Airport;
using TicketBooking.Class;
using TicketBooking.Country;

namespace TicketBooking.Flight.Services;

public class FlightValidator
{
    private List<FlightModel.Flight> ExistedFlights { get; init; }

    public FlightValidator(List<FlightModel.Flight> existedFlights)
    {
        ExistedFlights = existedFlights;
    }

    public string ValidateFlightProperties(
        int id,
        string departureCountry,
        string destinationCountry,
        Dictionary<string, double> flightClassDict,
        string flightNo,
        DateTime departureTime,
        string departureAirport,
        string arrivalAirport)
    {
        StringBuilder sb = new StringBuilder();

        if (!validateId(id)) sb.AppendLine("Id is invalid are duplicated, should be unique and greater than 0");

        if (!ValidateCountry(departureCountry)) sb.AppendLine("Departure country is not found");

        if (!ValidateCountry(destinationCountry)) sb.AppendLine("Destination country is not found");

        if (!ValidateAirport(departureAirport)) sb.AppendLine("Departure airport is not found");

        if (!ValidateAirport(arrivalAirport)) sb.AppendLine("Arrival airport is not found");

        if (!ValidateDepartureTime(departureTime)) sb.AppendLine("Invalid departure time, should be future date");

        if (!ValidateFlightClass(flightClassDict)) sb.AppendLine("Valid classes are: FirstClass, Business, Economy. Prices should be positive values too");

        return sb.ToString();
    }

    public bool validateId(int id)
    {
        try { return id > 0 && ExistedFlights.Count(flight => flight.Id == id) == 0; }
        catch { return false; }
    }

    public bool ValidateCountry(string country)
    {
        try
        {
            return
                new List<string>(Enum.GetNames(typeof(CountryEnum)))
                .SingleOrDefault(existedCountry => existedCountry.Equals(country))
                .Any();
        }
        catch { return false; }
    }

    public bool ValidateAirport(string airport)
    {
        try
        {
            return
                new List<string>(Enum.GetNames(typeof(AirportEnum)))
                .SingleOrDefault(existedAirport => existedAirport.Equals(airport))
                .Any();

        }
        catch { return false; }
    }

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
                if (!Enum.TryParse(flightClass.Key, out ClassEnum enumValue)) valid = false;
                if (flightClass.Value <= 0) valid = false;
            }

        }
        catch { valid = false; }
        return valid;
    }

}
