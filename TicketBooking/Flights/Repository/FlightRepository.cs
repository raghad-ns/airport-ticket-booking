using TicketBooking.Countries;
using TicketBooking.Classes;
using TicketBooking.Airports;
using TicketBooking.Flights.Services;
using TicketBooking.AppSettings;
using TicketBooking.FileProcessor.CSVPvocessor;
using TicketBooking.FileProcessor.Deserializer.Flight;

namespace TicketBooking.Flights.Repository;

public class FlightRepository
{
    private List<Models.Flight> Flights { get; set; } = new List<Models.Flight>();

    public FlightRepository()
    {
        LoadFlights();

        // Sure that flights data stored in the app file are all valid, so upload feedback cleared from the console
        Console.Clear();
    }

    public List<Models.Flight> FilterFlights(
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

    public List<Models.Flight> GetFlights()
    {
        return Flights;
    }

    public void DisplayFlights()
    {
        Console.WriteLine($"Available flights: ");

        foreach (var flight in Flights)
        {
            Console.WriteLine(flight.ToString());
        }
    }

    public void LoadFlights(string? path = null)
    {
        // Assign this value if path is null
        path ??= AppSettingsInitializer.AppSettingsInstance().FlightsRepoPath;
        var lines = CSVFilesProcessor.Load(path);
        Console.WriteLine($"records: {lines.Count}");
        foreach (var values in lines)
        {
            Console.WriteLine("Processing...");
            //var (
            //    id,
            //    departureCountry,
            //    destinationCountry,
            //    flightNo,
            //    departureDate,
            //    departureAirport,
            //    arrivalAirport) = (
            //        int.Parse(values[0]),
            //        values[1],
            //        values[2],
            //        values[6],
            //        DateTime.Parse(values[7]),
            //        values[8],
            //        values[9]);

            //Dictionary<string, double> flightClassesDict = new();

            //if (!string.IsNullOrWhiteSpace(values[3])) flightClassesDict.Add("FirstClass", double.Parse(values[3]));

            //if (!string.IsNullOrWhiteSpace(values[4])) flightClassesDict.Add("Business", double.Parse(values[4]));

            //if (!string.IsNullOrWhiteSpace(values[5])) flightClassesDict.Add("Economy", double.Parse(values[5]));

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

            Dictionary<Class, double> classesDict = new Dictionary<Class, double>();

            foreach (var c in DeserializedFlightData.flightClassesDict)
            {
                classesDict.Add((Class)Enum.Parse(typeof(Class), c.Key), c.Value);
            }

            var flight = new Models.Flight()
            {
                Id = DeserializedFlightData.id,
                DepartureCountry = (Country)Enum.Parse(typeof(Country), DeserializedFlightData.departureCountry),
                DestinationCountry = (Country)Enum.Parse(typeof(Country), DeserializedFlightData.destinationCountry),
                Class = classesDict,
                FlightNo = DeserializedFlightData.flightNo,
                DepartureDate = DeserializedFlightData.departureDate,
                DepartureAirport = (Airport)Enum.Parse(typeof(Airport), DeserializedFlightData.departureAirport),
                ArrivalAirport = (Airport)Enum.Parse(typeof(Airport), DeserializedFlightData.arrivalAirport),
            };

            Flights.Add(flight);
            Console.WriteLine($"Flight holding the id: {DeserializedFlightData.id} uploaded successfully!");
        }

    }
}