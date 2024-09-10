using TicketBooking.Countries;
using TicketBooking.Classes;
using TicketBooking.Airports;
using TicketBooking.Flights.Services;
using TicketBooking.AppSettings;
using TicketBooking.FileProcessor.Deserializer.Flight;
using TicketBooking.FileProcessor.CSVProcessor;
using TicketBooking.Flights.Models;

namespace TicketBooking.Flights.Repository;

public class FlightRepository
{
    private List<Flight> Flights { get; set; }
    private static FlightRepository _instance ;

    private FlightRepository(List<Flight> flights)
    {
        Flights = flights;
        // Sure that flights data stored in the app file are all valid, so upload feedback cleared from the console
        Console.Clear();
    }

    public static FlightRepository GetInstance(List<Flight> flights)
    {
        _instance ??= new(flights);
        return _instance;
    }

    public List<Flight> FilterFlights(
        double? priceFrom = null,
        double? priceTo = null,
        string? departureCountry = null,
        string? destinationCountry = null,
        string? departureAirport = null,
        string? arrivalAirport = null,
        Class? flightClass = null
        )
    {
        IEnumerable<Models.Flight> tempFlights = Flights;

        if (departureCountry is not null)
        {
            tempFlights = tempFlights.Where(flight => flight.DepartureCountry.Equals((Country)Enum.Parse(typeof(Country), departureCountry)));
        }

        if (destinationCountry is not null)
        {
            tempFlights = tempFlights.Where(flight => flight.DestinationCountry.Equals((Country)Enum.Parse(typeof(Country), destinationCountry)));
        }

        if (departureAirport is not null)
        {
            tempFlights = tempFlights.Where(flight => flight.DepartureAirport.Equals((Airport)Enum.Parse(typeof(Airport), departureAirport)));
        }

        if (arrivalAirport is not null)
        {
            tempFlights = tempFlights.Where(flight => flight.ArrivalAirport.Equals((Airport)Enum.Parse(typeof(Airport), arrivalAirport)));
        }

        if (flightClass is not null)
        {
            tempFlights = tempFlights
                .Where(flight => flight.Class.Any(availableClass => availableClass.Key.Equals(flightClass)));
        }

        if (priceFrom is not null)
        {
            tempFlights = tempFlights.Where(flight => flight.Class.Any(availableClass => availableClass.Value >= priceFrom));
        }

        if (priceTo is not null)
        {
            tempFlights = tempFlights.Where(flight => flight.Class.Any(availableClass => availableClass.Value <= priceTo));
        }

        return tempFlights.ToList();
    }

    public List<Flight> GetFlights()
    {
        return Flights;
    }

    public void LoadFlights(string? path = null)
    {
        // Assign this value if path is null
        path ??= AppSettingsInitializer.AppSettingsInstance().FlightsRepoPath;
        var lines = CSVProcessor.Load(path);

        foreach (var values in lines)
        {
            var DeserializedFlightData = FlightDeserializer.Deserialize(values);

            var validator = new FlightValidator(Flights);
            string validationResult = validator.ValidateFlightProperties(DeserializedFlightData);

            // If flight's data holding invalid values
            if (validationResult.Length > 0)
            {
                Console.WriteLine($"About flight holding the id: {DeserializedFlightData.id}");
                Console.WriteLine(validationResult);
                continue;
            }

            Flights.Add(FlightServices.GetFlightObject(DeserializedFlightData));
            Console.WriteLine($"Flight holding the id: {DeserializedFlightData.id} uploaded successfully!");
        }
    }
}