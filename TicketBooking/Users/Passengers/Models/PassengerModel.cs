using TicketBooking.Users.Passengers.Bookings;

namespace TicketBooking.Users.Passengers.Models;

public class PassengerModel : UserModel
{
    public List<BookingsModel> PersonalFlights { get; set; } = new List<BookingsModel>();
}