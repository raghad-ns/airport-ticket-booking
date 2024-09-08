using TicketBooking.Airports;
using TicketBooking.Classes;
using TicketBooking.Countries;

namespace TicketBooking.Flights.Models;

public class Flight
{
    public int Id { get; set; }
    public Country DepartureCountry { get; set; }
    public Country DestinationCountry { get; set; }
    public Dictionary<Class, double> Class { get; set; }
    public string FlightNo { get; set; }
    public DateTime DepartureDate { get; set; }
    public Airport DepartureAirport { get; set; }
    public Airport ArrivalAirport { get; set; }

    public override string ToString()
    {
        return $"Flight ID: {Id}: Flight No {FlightNo}, departs from {DepartureCountry}, {DepartureAirport}, to {DestinationCountry}, {ArrivalAirport} at {DepartureDate.ToString()}";
    }
}