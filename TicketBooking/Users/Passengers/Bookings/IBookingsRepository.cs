using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Users.Passengers.Bookings;

public interface IBookingsRepository
{
    void AddBookingToFile(BookingsModel booking);
    void RemoveBookingFromFile(int id);
    void UpdateBooking(BookingsModel newBooking);
}
