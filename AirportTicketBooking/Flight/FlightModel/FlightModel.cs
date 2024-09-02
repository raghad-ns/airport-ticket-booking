using AirportTicketBooking.Airport;
using AirportTicketBooking.Class;
using AirportTicketBooking.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking.Flight.FlightModel
{
    public class Flight
    {
        public int Id { get; set; }
        public CountryEnum DepartureCountry { get; set; }
        public CountryEnum DestinationCountry { get; set; }
        public ClassEnum Class { get; set; }
        public string FlightNo { get; set; }
        public DateTime DepartureDate { get; set; }
        public AirportEnum DepartureAirport { get; set; }
        public AirportEnum ArrivalAirport { get; set; }

        public override string ToString()
        {
            return $"Flight No: {FlightNo}, departs from {DepartureCountry}, {DepartureAirport}, to {DestinationCountry}, {ArrivalAirport} at {DepartureDate.ToString()}";
        }
    }
}
