using System.Text;
using TicketBooking.Classes;

namespace TicketBooking.Flights.Services;

public class FlightPrinter
{
    public static void DisplayFlights(List<Flights.Models.Flight> flights)
    {
        Console.WriteLine($"Available flights: ");

        foreach (var flight in flights)
        {
            Console.WriteLine(flight.ToString());
        }
    }

    public static string GetFlightDetails(Flights.Models.Flight flight)
    {
        return flight.ToString() + '\n' + GetFlightClassesAndPrices(flight);
    }   

    public static string GetFlightClassesAndPrices(Flights.Models.Flight flight)
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
