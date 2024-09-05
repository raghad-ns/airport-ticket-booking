using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Class;
using TicketBooking.Flight.FlightModel;

namespace TicketBooking.Flight.Services;

public class FlightPrinter
{
    public static void DisplayFlights(List<FlightModel.Flight> flights)
    {
        Console.WriteLine($"Available flights: ");

        foreach (var flight in flights)
        {
            Console.WriteLine(flight.ToString());
        }
    }

    public static string GetFlightDetails(FlightModel.Flight flight)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(flight.ToString());
        stringBuilder.AppendLine(GetFlightClassesAndPrices(flight));
        return stringBuilder.ToString();
    }   

    public static string GetFlightClassesAndPrices(FlightModel.Flight flight)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Available classes: ");

        foreach (KeyValuePair<ClassEnum, double> flightClass in flight.Class)
        {
            stringBuilder.AppendLine($"{Enum.GetName(typeof(ClassEnum), flightClass.Key)} => ${flightClass.Value}.");
        }

        return stringBuilder.ToString();
    } 
}
