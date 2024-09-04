using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Class;

namespace TicketBooking.User.Passenger.Bookings
{
    public class BookingsService
    {
        public List<BookingsModel> Bookings { get; init; }

        // TODO: update it to save booking to the specified file
        public void AddBooking(Flight.FlightModel.Flight flight, ClassEnum flightClass)
        {
            Bookings.Add(new BookingsModel()
            {
                Id = Bookings.Count + 1,
                Flight = flight,
                ChosenClass = flightClass
            });
        }

        public void CancelBooking(int id)
        {
            Bookings.RemoveAt(id - 1);
        }

        public void UpdateBooking(int id, ClassEnum newClass)
        {
            Bookings[id - 1].ChosenClass = newClass;
        }
    }
}
