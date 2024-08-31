using AirportTicketBooking.Country;
using AirportTicketBooking.Flight.FlightRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FlightRepository repository = new FlightRepository();
            foreach(var record in repository.Flights)
            {
                Console.WriteLine(record.Id);
            }

        }
    }
}
