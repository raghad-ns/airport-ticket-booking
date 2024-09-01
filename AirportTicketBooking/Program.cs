using AirportTicketBooking.Country;
using AirportTicketBooking.Flight.FlightRepository;
using AirportTicketBooking.User.Passenger.PassengerModel;
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
            //FlightRepository repository = new FlightRepository();
            //foreach(var record in repository.Flights)
            //{
            //    Console.WriteLine(record.Id);
            //}
            User.UserRepository.UserRepository userRepository = new User.UserRepository.UserRepository();
            foreach (var record in userRepository.Users)
            {
                Console.WriteLine(record is PassengerModel ? "Passenger" : "Manager");
            }

        }
    }
}
