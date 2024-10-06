using System.Text;
using TicketBooking.Classes;

namespace TicketBooking.Users.Passengers.Bookings;

public class BookingsModel
{
    public int Id { get; set; }
    public int UserId { get; init; }
    public Flights.Models.Flight Flight { get; set; }
    public Class ChosenClass { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Booking Id: {Id}");
        sb.AppendLine(Flight.ToString());
        sb.AppendLine($"Class: {ChosenClass}");
        return sb.ToString();
    }
}