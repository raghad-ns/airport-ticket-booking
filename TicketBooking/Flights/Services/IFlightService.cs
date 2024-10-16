using TicketBooking.Classes;
using TicketBooking.FileProcessor.Deserializer.Flight;
using TicketBooking.Flights.Models;

namespace TicketBooking.Flights.Services;

public interface IFlightService
{
    List<Flight> FilterFlights(
        double? priceFrom = null,
        double? priceTo = null,
        string? departureCountry = null,
        string? destinationCountry = null,
        string? departureAirport = null,
        string? arrivalAirport = null,
        Class? flightClass = null
        );
    List<Flight> LoadFlights(string path);
    void DisplayFlights();
    List<Flight> GetFlights();
    string GetFlightClassesAndPricesForDisplay(Flight flight);
    string GetFlightDetailsForDisplay(Flight flight);
}