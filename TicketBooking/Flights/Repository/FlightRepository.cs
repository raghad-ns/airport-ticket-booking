using TicketBooking.Countries;
using TicketBooking.Classes;
using TicketBooking.Airports;
using TicketBooking.Flights.Services;
using TicketBooking.AppSettings;
using TicketBooking.FileProcessor.Deserializer.Flight;
using TicketBooking.FileProcessor.CSVProcessor;
using TicketBooking.Flights.Models;
using TicketBooking.Users.Passengers.Bookings;

namespace TicketBooking.Flights.Repository;

public class FlightRepository: IFlightRepository
{
    private List<Flight> Flights { get; set; }
    private static FlightRepository _instance;
    private static readonly string filePath = AppSettingsInitializer.AppSettingsInstance().FlightsRepoPath;

    private FlightRepository(List<Flight> flights)
    {
        Flights = flights;
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

            //Flight flight = (new FlightServices(Flights,this, new FlightPrinter())).GetFlightObject(DeserializedFlightData);
            Flight flight = FlightServices.GetFlightObject(DeserializedFlightData);
            Flights.Add(flight);
            AddFlightToFile(flight);

            Console.WriteLine($"Flight holding the id: {DeserializedFlightData.id} uploaded successfully!");
        }
    }

    public static void AddFlightToFile(Flight flight)
    {
        List<string> lines = new List<string>();

        lines.AddRange(File.ReadAllLines(filePath));
        double price;

        string newLine =
            flight.Id + "," +
            flight.DepartureCountry + "," +
            flight.DepartureCountry + "," +
            (flight.Class.TryGetValue(Class.FirstClass, out price)? price.ToString() : "") + "," +
            (flight.Class.TryGetValue(Class.Business, out price) ? price.ToString() : "") + "," +
            (flight.Class.TryGetValue(Class.Economy, out price) ? price.ToString() : "") + "," +
            flight.FlightNo + "," +
            flight.DepartureDate + "," +
            flight.DepartureAirport + "," +
            flight.ArrivalAirport;

        lines.Add(newLine);

        File.WriteAllLines(filePath, lines);
    }
}