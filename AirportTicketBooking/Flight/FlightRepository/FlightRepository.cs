using System;
using System.Collections.Generic;
using IronXL;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBooking.Flight.FlightModel;
using AirportTicketBooking.Country;
using AirportTicketBooking.Class;
using AirportTicketBooking.Airport;

namespace AirportTicketBooking.Flight.FlightRepository
{
    internal class FlightRepository
    {
        public List<FlightModel.Flight> Flights { get; private set; } = new List<FlightModel.Flight>();

        public FlightRepository()
        {
            UploadFlights();
        }
        public List<FlightModel.Flight> GetFlight()
        {
            // TODO: filter flights and return matched objects
            return Flights;
        }

        private void UploadFlights()
        {
            const string path = "C:\\Users\\M.T\\Desktop\\projects\\foothill\\practice-projects\\AirportTicketBooking\\AirportTicketBooking\\Flight\\FlightRepository\\Flights.csv";
            using (var reader = new StreamReader(path))
            {
                var headerLine = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    //Console.WriteLine($"line: {line}");

                    var flight = new FlightModel.Flight()
                    {
                        Id = int.Parse(values[0]),
                        DepartureCountry= (CountryEnum)Enum.Parse(typeof(CountryEnum), values[1]),
                        DestinationCountry= (CountryEnum)Enum.Parse(typeof(CountryEnum), values[2]),
                        Class = (ClassEnum)Enum.Parse(typeof(ClassEnum), values[3]),
                        FlightNo = values[4],
                        DepartureDate = DateTime.Parse(values[5]),
                        DepartureAirport = (AirportEnum)Enum.Parse(typeof (AirportEnum), values[6]),
                        ArrivalAirport = (AirportEnum)Enum.Parse(typeof (AirportEnum), values[7]),
                    };

                    Flights.Add(flight);
                }
            }
        }
    }
}
