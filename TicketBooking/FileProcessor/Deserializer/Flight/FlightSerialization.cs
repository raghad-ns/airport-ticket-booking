using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.FileProcessor.Deserializer.Flight
{
    public record FlightSerialization(
        int id,
        string departureCountry,
        string destinationCountry,
        string flightNo,
        Dictionary<string, double> flightClassesDict,
        DateTime departureDate,
        string departureAirport,
        string arrivalAirport);

}
