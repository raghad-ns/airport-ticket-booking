using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Class;

namespace TicketBooking.User.Passenger.Bookings;

public class BookingsModel
{
    public int Id { get; set; }
    public int UserId { get; init; }
    public Flight.FlightModel.Flight Flight { get; set; }
    public ClassEnum ChosenClass { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Booking Id: {Id}");
        sb.AppendLine(Flight.ToString());
        sb.AppendLine($"Class: {ChosenClass}");
        return sb.ToString();
    }
}