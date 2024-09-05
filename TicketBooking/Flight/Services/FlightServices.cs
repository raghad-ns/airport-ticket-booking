using TicketBooking.Airport;
using TicketBooking.Class;
using TicketBooking.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Flight.Services;

public class FlightServices
{
    private FlightRepository.FlightRepository _flightRepository = new();
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
        FlightPrinter.DisplayFlights(_flightRepository.GetFlights());
    }

    public List<FlightModel.Flight> GetFlights()
    {
        return _flightRepository.GetFlights();
    }

    public string GetFlightClassesAndPrices(FlightModel.Flight flight)
    {
        return FlightPrinter.GetFlightClassesAndPrices(flight);
    }

    public string GetFlightDetails(FlightModel.Flight flight)
    {
        return FlightPrinter.GetFlightDetails(flight);
    }
}
