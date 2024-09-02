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
using AirportTicketBooking.Flight.Services;

namespace AirportTicketBooking.Flight.FlightRepository
{
    public class FlightRepository
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

        public List<FlightModel.Flight> GetFlights()
        {
            return Flights;
        }

        public void UploadFlights(string path = "C:\\Users\\M.T\\Desktop\\projects\\foothill\\practice-projects\\AirportTicketBooking\\AirportTicketBooking\\Flight\\FlightRepository\\Flights.csv")
        {
            using (var reader = new StreamReader(path))
            {
                var headerLine = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    var (
                        id,
                        departureCountry,
                        destinationCountry,
                        flightNo,
                        departureDate,
                        departureAirport,
                        arrivalAirport) = (
                            int.Parse(values[0]),
                            values[1],
                            values[2],
                            values[6],
                            DateTime.Parse(values[7]),
                            values[8],
                            values[9]);

                    Dictionary<string, double> flightClasses = new Dictionary<string, double>();
                    if (!string.IsNullOrWhiteSpace(values[3])) flightClasses.Add("FirstClass", double.Parse(values[3]));
                    if (!string.IsNullOrWhiteSpace(values[4])) flightClasses.Add("Business", double.Parse(values[4]));
                    if (!string.IsNullOrWhiteSpace(values[5])) flightClasses.Add("Economy", double.Parse(values[5]));
                    var flightService = new FlightServices() { ExistedFlights = Flights };
                    if (flightService.ValidateFlightProperties(id, departureCountry, destinationCountry, flightClasses, flightNo, departureDate, departureAirport, arrivalAirport).Count == 0)
                    {
                        Dictionary<ClassEnum, double> classes = new Dictionary<ClassEnum, double>();
                        foreach (var c in flightClasses)
                        {
                            classes.Add((ClassEnum)Enum.Parse(typeof(ClassEnum), c.Key), c.Value);
                        }

                        var flight = new FlightModel.Flight()
                        {
                            Id = id,
                            DepartureCountry = (CountryEnum)Enum.Parse(typeof(CountryEnum), departureCountry),
                            DestinationCountry = (CountryEnum)Enum.Parse(typeof(CountryEnum), destinationCountry),
                            Class = classes,
                            FlightNo = flightNo,
                            DepartureDate = departureDate,
                            DepartureAirport = (AirportEnum)Enum.Parse(typeof(AirportEnum), departureAirport),
                            ArrivalAirport = (AirportEnum)Enum.Parse(typeof(AirportEnum), arrivalAirport),
                        };

                        Flights.Add(flight);
                    }

                }
            }
        }
    }
}
