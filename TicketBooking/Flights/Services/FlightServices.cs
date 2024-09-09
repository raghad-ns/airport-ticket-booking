using TicketBooking.Classes;
using TicketBooking.Flights.Repository;

namespace TicketBooking.Flights.Services;

public class FlightServices
{
    private FlightRepository _flightRepository = FlightRepository.GetInstance();
    public List<Flights.Models.Flight> FilterFlights(
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

    public List<Flights.Models.Flight> GetFlights()
    {
        return _flightRepository.GetFlights();
    }

    public string GetFlightClassesAndPricesForDisplay(Flights.Models.Flight flight)
    {
        return FlightPrinter.GetFlightClassesAndPrices(flight);
    }

    public string GetFlightDetailsForDisplay(Flights.Models.Flight flight)
    {
        return FlightPrinter.GetFlightDetails(flight);
    }
}