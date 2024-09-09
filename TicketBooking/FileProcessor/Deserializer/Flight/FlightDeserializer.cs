using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Flights.Models;

namespace TicketBooking.FileProcessor.Deserializer.Flight;
public class FlightDeserializer : IDeserialize
{
    public static FlightSerialization Deserialize(string[] flightData)
    {
        Dictionary<string, double> flightClassesDict = new();

        if (!string.IsNullOrWhiteSpace(flightData[3])) flightClassesDict.Add("FirstClass", double.Parse(flightData[3]));

        if (!string.IsNullOrWhiteSpace(flightData[4])) flightClassesDict.Add("Business", double.Parse(flightData[4]));

        if (!string.IsNullOrWhiteSpace(flightData[5])) flightClassesDict.Add("Economy", double.Parse(flightData[5]));
        return new FlightSerialization(
                int.Parse(flightData[0]),
                flightData[1],
                flightData[2],
                flightData[6], flightClassesDict,
                DateTime.Parse(flightData[7]),
                flightData[8],
                flightData[9]);


    }
}
