using TicketBooking.Airport;
using TicketBooking.Class;
using TicketBooking.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Flight.FlightModel
{
    public class Flight
    {
        public int Id { get; set; }
        public CountryEnum DepartureCountry { get; set; }
        public CountryEnum DestinationCountry { get; set; }
        public Dictionary<ClassEnum, double> Class { get; set; }
        public string FlightNo { get; set; }
        public DateTime DepartureDate { get; set; }
        public AirportEnum DepartureAirport { get; set; }
        public AirportEnum ArrivalAirport { get; set; }

        public override string ToString()
        {
            return $"Flight ID: {Id}: Flight No {FlightNo}, departs from {DepartureCountry}, {DepartureAirport}, to {DestinationCountry}, {ArrivalAirport} at {DepartureDate.ToString()}";
        }

        public string GetFlightDetails()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(ToString());
            stringBuilder.AppendLine(GetFlightClassesAndPrices());
            return stringBuilder.ToString();
        }

        public string GetFlightClassesAndPrices()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Available classes: ");
            foreach (KeyValuePair<ClassEnum, double> flightClass in Class)
            {
                stringBuilder.AppendLine($"{Enum.GetName(typeof(ClassEnum), flightClass.Key)} => ${flightClass.Value}.");
            }
            return stringBuilder.ToString();

        }
    }
}
