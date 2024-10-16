using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Airports;
using TicketBooking.Classes;
using TicketBooking.Countries;
using TicketBooking.Flights.Models;

namespace TicketBooking.Flights.Repository;

public interface IFlightRepository
{
    public List<Flight> FilterFlights(
        double? priceFrom = null,
        double? priceTo = null,
        string? departureCountry = null,
        string? destinationCountry = null,
        string? departureAirport = null,
        string? arrivalAirport = null,
        Class? flightClass = null
        );
    public List<Flight> GetFlights();
    public List<Flight> LoadFlights(string? path = null);
}
