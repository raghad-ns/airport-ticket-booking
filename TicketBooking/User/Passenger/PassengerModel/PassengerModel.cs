using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.User.Passenger.Bookings;

namespace TicketBooking.User.Passenger.PassengerModel;

public class PassengerModel : UserModel
{
    public List<BookingsModel> PersonalFlights { get; set; } = new List<BookingsModel>();
}
