using TicketBooking.Flights.Models;

namespace TicketBooking.Flights.Services;

public interface IFlightPrinter
{
    void DisplayFlights(List<Flight> flights);
    string GetFlightDetails(Flight flight);
    string GetFlightClassesAndPrices(Flight flight);
}