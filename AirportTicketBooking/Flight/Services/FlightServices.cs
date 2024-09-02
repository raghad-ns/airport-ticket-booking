using AirportTicketBooking.Airport;
using AirportTicketBooking.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking.Flight.Services
{
    public class FlightServices
    {
        public List<FlightModel.Flight> ExistedFlights { get; set; }
        public List<string> ValidateFlightProperties(
            int id,
            string departureCountry,
            string destinationCountry,
            string flightClass,
            string flightNo,
            DateTime dapartureTime,
            string departureAirport,
            string arrivalAirport)
        {
            //Console.WriteLine(validateId(id) ? "valid id" : "invalid id");
            return new List<string>();
        }

        public bool validateId(int id)
        {
            return ExistedFlights.Count(flight => flight.Id == id) == 0;
        }

        public bool ValidateCountry(string country)
        {
            return
                new List<string>(Enum.GetNames(typeof(CountryEnum)))
                .SingleOrDefault(existedCountry => existedCountry.Equals(country))
                .Any();
        }

        public bool ValidateAirport(string airport)
        {
            return
                new List<string>(Enum.GetNames(typeof(AirportEnum)))
                .SingleOrDefault(existedAirport => existedAirport.Equals(airport))
                .Any();
        }

        public bool ValidateDepartureTime(DateTime dapartureTime)
        {
            return dapartureTime >= DateTime.Now;
        }

        //public bool ValidateFlightClass(string classes)
    }
}
