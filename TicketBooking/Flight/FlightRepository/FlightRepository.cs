using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Flight.FlightModel;
using TicketBooking.Country;
using TicketBooking.Class;
using TicketBooking.Airport;
using TicketBooking.Flight.Services;
using TicketBooking.AppSettings;

namespace TicketBooking.Flight.FlightRepository;

public class FlightRepository
{
    private List<FlightModel.Flight> Flights { get; set; } = new List<FlightModel.Flight>();

    public FlightRepository()
    {
        LoadFlights();

        // Sure that flights data stored in the app file are all valid, so upload feedback cleared from the console
        Console.Clear();
    }

    public List<Flight.FlightModel.Flight> FilterFlights(
        double? priceFrom = null,
        double? priceTo = null,
        string? departureCountry = null,
        string? destinationCountry = null,
        string? departureAirport = null,
        string? arrivalAirport = null,
        ClassEnum? flightClass = null
        )
    {
        List<Flight.FlightModel.Flight> tempFlights = Flights;

        if (departureCountry is not null)
        {
            tempFlights = tempFlights.Where(flight => flight.DepartureCountry.Equals((CountryEnum)Enum.Parse(typeof(CountryEnum), departureCountry))).ToList();
        }

        if (destinationCountry is not null)
        {
            tempFlights = tempFlights.Where(flight => flight.DestinationCountry.Equals((CountryEnum)Enum.Parse(typeof(CountryEnum), destinationCountry))).ToList();
        }

        if (departureAirport is not null)
        {
            tempFlights = tempFlights.Where(flight => flight.DepartureAirport.Equals((AirportEnum)Enum.Parse(typeof(AirportEnum), departureAirport))).ToList();
        }

        if (arrivalAirport is not null)
        {
            tempFlights = tempFlights.Where(flight => flight.ArrivalAirport.Equals((AirportEnum)Enum.Parse(typeof(AirportEnum), arrivalAirport))).ToList();
        }

        if (flightClass is not null)
        {
            tempFlights = tempFlights
                .Where(flight => flight.Class.Any(availableClass => availableClass.Key.Equals(flightClass)))
                .ToList();
        }

        if (priceFrom is not null)
        {
            tempFlights = tempFlights.Where(flight => flight.Class.Any(availableClass => availableClass.Value >= priceFrom)).ToList();
        }

        if (priceTo is not null)
        {
            tempFlights = tempFlights.Where(flight => flight.Class.Any(availableClass => availableClass.Value <= priceTo)).ToList();
        }

        return tempFlights;
    }

    public List<FlightModel.Flight> GetFlights()
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
        if (path is null) path = AppSettingsInitializer.AppSettingsInstance.FlightsRepoPath;
        using (var reader = new StreamReader(path))
        {
            var headerLine = reader.ReadLine();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                var (
                    id,
                    departureCountry,
                    destinationCountry,
                    flightNo,
                    departureDate,
                    departureAirport,
                    arrivalAirport) = (
                        int.Parse(values[0]),
                        values[1],
                        values[2],
                        values[6],
                        DateTime.Parse(values[7]),
                        values[8],
                        values[9]);

                Dictionary<string, double> flightClassesDict = new Dictionary<string, double>();

                if (!string.IsNullOrWhiteSpace(values[3])) flightClassesDict.Add("FirstClass", double.Parse(values[3]));

                if (!string.IsNullOrWhiteSpace(values[4])) flightClassesDict.Add("Business", double.Parse(values[4]));

                if (!string.IsNullOrWhiteSpace(values[5])) flightClassesDict.Add("Economy", double.Parse(values[5]));

                var validator = new FlightValidator(Flights);
                string validationResult = validator.ValidateFlightProperties(
                    id,
                    departureCountry,
                    destinationCountry,
                    flightClassesDict,
                    flightNo,
                    departureDate,
                    departureAirport,
                    arrivalAirport);

                if (validationResult.Count() == 0)
                {
                    Dictionary<ClassEnum, double> classesDict = new Dictionary<ClassEnum, double>();

                    foreach (var c in flightClassesDict)
                    {
                        classesDict.Add((ClassEnum)Enum.Parse(typeof(ClassEnum), c.Key), c.Value);
                    }

                    var flight = new FlightModel.Flight()
                    {
                        Id = id,
                        DepartureCountry = (CountryEnum)Enum.Parse(typeof(CountryEnum), departureCountry),
                        DestinationCountry = (CountryEnum)Enum.Parse(typeof(CountryEnum), destinationCountry),
                        Class = classesDict,
                        FlightNo = flightNo,
                        DepartureDate = departureDate,
                        DepartureAirport = (AirportEnum)Enum.Parse(typeof(AirportEnum), departureAirport),
                        ArrivalAirport = (AirportEnum)Enum.Parse(typeof(AirportEnum), arrivalAirport),
                    };

                    Flights.Add(flight);
                    Console.WriteLine($"Flight holding the id: {id} uploaded successfully!");
                }
                else
                {
                    Console.WriteLine($"About flight holding the id: {id}");
                    Console.WriteLine(validationResult);
                }
            }
        }
    }
}