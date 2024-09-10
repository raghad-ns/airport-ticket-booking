using TicketBooking.Airports;
using TicketBooking.Classes;
using TicketBooking.Countries;
using TicketBooking.FileProcessor.Deserializer.Flight;
using TicketBooking.Flights.Models;
using TicketBooking.Flights.Repository;

namespace TicketBooking.Flights.Services;

public class FlightServices
{
    private FlightRepository _flightRepository;

    public FlightServices(List<Flight> flights)
    {
        _flightRepository = FlightRepository.GetInstance(flights);
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

    public void LoadFlights(string path)
    {
        _flightRepository.LoadFlights(path);
    }

    public void DisplayFlights()
    {
        FlightPrinter.DisplayFlights(GetFlights());
    }

    public List<Flight> GetFlights()
    {
        return _flightRepository.GetFlights();
    }

    public string GetFlightClassesAndPricesForDisplay(Flight flight)
    {
        return FlightPrinter.GetFlightClassesAndPrices(flight);
    }

    public string GetFlightDetailsForDisplay(Flight flight)
    {
        return FlightPrinter.GetFlightDetails(flight);
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