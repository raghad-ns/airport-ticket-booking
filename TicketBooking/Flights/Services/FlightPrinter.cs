using System.Text;
using TicketBooking.Classes;
using TicketBooking.Flights.Models;

namespace TicketBooking.Flights.Services;

public class FlightPrinter: IFlightPrinter
{
    public void DisplayFlights(List<Flight> flights)
    {
        Console.WriteLine($"Available flights: ");

        foreach (var flight in flights)
        {
            Console.WriteLine(flight.ToString());
        }
    }

    public string GetFlightDetails(Flight flight)
    {
        return flight.ToString() + '\n' + GetFlightClassesAndPrices(flight);
    }   

    public string GetFlightClassesAndPrices(Flight flight)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Available classes: ");

        foreach (KeyValuePair<Class, double> flightClass in flight.Class)
        {
            stringBuilder.AppendLine($"{Enum.GetName(typeof(Class), flightClass.Key)} => ${flightClass.Value}.");
        }

        return stringBuilder.ToString();
    } 
}