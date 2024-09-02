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
                        flightClass,
                        flightNo,
                        departureDate,
                        departureAirport,
                        arrivalAirport) = (
                            int.Parse(values[0]),
                            values[1],
                            values[2],
                            values[3],
                            values[4],
                            DateTime.Parse(values[5]),
                            values[6],
                            values[7]);

                    var flightService = new FlightServices() { ExistedFlights = Flights};
                    if (flightService.ValidateFlightProperties(id, departureCountry, destinationCountry, flightClass, flightNo, departureDate, departureAirport, arrivalAirport).Count == 0)
                    {
                        //Console.WriteLine("valid flight");

                        List<string> classes = flightClass.Split(' ').ToList();
                        ClassEnum classEnum = new ClassEnum();
                        foreach (var c in classes)
                        {
                            ClassEnum className = classEnum | (ClassEnum)Enum.Parse(typeof(ClassEnum), c);
                        }

                        var flight = new FlightModel.Flight()
                        {
                            Id = int.Parse(values[0]),
                            DepartureCountry = (CountryEnum)Enum.Parse(typeof(CountryEnum), values[1]),
                            DestinationCountry = (CountryEnum)Enum.Parse(typeof(CountryEnum), values[2]),
                            Class = classEnum,
                            FlightNo = values[4],
                            DepartureDate = DateTime.Parse(values[5]),
                            DepartureAirport = (AirportEnum)Enum.Parse(typeof(AirportEnum), values[6]),
                            ArrivalAirport = (AirportEnum)Enum.Parse(typeof(AirportEnum), values[7]),
                        };

                        Flights.Add(flight);
                    }

                }
            }
        }
    }
}
