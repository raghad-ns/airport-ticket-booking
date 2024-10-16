using TicketBooking.Airports;
using TicketBooking.Classes;
using TicketBooking.Countries;
using TicketBooking.FileProcessor.Deserializer.Flight;
using TicketBooking.Flights.Models;
using TicketBooking.Flights.Repository;

namespace TicketBooking.Flights.Services;

public class FlightServices: IFlightService
{
    private IFlightRepository _flightRepository;
    private readonly IFlightPrinter _flightPrinter;

    public FlightServices(List<Flight> flights, IFlightRepository flightRepository, IFlightPrinter flightPrinter)
    {
        _flightRepository = flightRepository;
        _flightPrinter = flightPrinter;
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
        return _flightRepository.FilterFlights(
            priceFrom,
            priceTo,
            departureCountry,
            destinationCountry,
            departureAirport,
            arrivalAirport,
            flightClass
            );
    }

    public List<Flight> LoadFlights(string path)
    {
        return _flightRepository.LoadFlights(path);
    }

    public void DisplayFlights()
    {
        _flightPrinter.DisplayFlights(GetFlights());
    }

    public List<Flight> GetFlights()
    {
        return _flightRepository.GetFlights();
    }

    public string GetFlightClassesAndPricesForDisplay(Flight flight)
    {
        return _flightPrinter.GetFlightClassesAndPrices(flight);
    }

    public string GetFlightDetailsForDisplay(Flight flight)
    {
        return _flightPrinter.GetFlightDetails(flight);
    }

    public static Flight GetFlightObject(FlightSerialization flight)
    {
        Dictionary<Class, double> classesDict = new Dictionary<Class, double>();

        foreach (var c in flight.flightClassesDict)
        {
            classesDict.Add((Class)Enum.Parse(typeof(Class), c.Key), c.Value);
        }

        return new Flight()
        {
            Id = flight.id,
            DepartureCountry = (Country)Enum.Parse(typeof(Country), flight.departureCountry),
            DestinationCountry = (Country)Enum.Parse(typeof(Country), flight.destinationCountry),
            Class = classesDict,
            FlightNo = flight.flightNo,
            DepartureDate = flight.departureDate,
            DepartureAirport = (Airport)Enum.Parse(typeof(Airport), flight.departureAirport),
            ArrivalAirport = (Airport)Enum.Parse(typeof(Airport), flight.arrivalAirport),
        };
    }
}