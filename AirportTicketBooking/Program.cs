using AirportTicketBooking.Country;
using AirportTicketBooking.Flight.FlightRepository;
using AirportTicketBooking.User.Passenger.PassengerModel;
using AirportTicketBooking.UserInterface;
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
            UserInterface.UserInterface userInterface = new UserInterface.UserInterface();
            userInterface.ShowInitialMenu();
        }
    }
}
