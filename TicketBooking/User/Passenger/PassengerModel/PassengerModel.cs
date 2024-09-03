using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.User.Passenger.PassengerModel
{
    public class PassengerModel : UserModel
    {
        public List<Flight.FlightModel.Flight> PersonalFlights { get; set; } = new List<Flight.FlightModel.Flight>();
    }
}
