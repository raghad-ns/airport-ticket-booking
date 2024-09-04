using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Class;

namespace TicketBooking.User.Passenger.Bookings
{
    public class BookingsModel
    {
        public int Id { get; set; }
        public Flight.FlightModel.Flight Flight { get; set; }
        public ClassEnum ChosenClass { get; set; }
    }
}
