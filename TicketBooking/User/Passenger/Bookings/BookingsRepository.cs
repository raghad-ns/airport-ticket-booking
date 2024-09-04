using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.User.Passenger.Bookings
{
    public class BookingsRepository
    {
        public List<BookingsModel> Bookings { get; set; }
        private static string filePath = "C:\\Users\\M.T\\Desktop\\projects\\foothill\\practice-projects\\AirportTicketBooking\\TicketBooking\\User\\UserRepository\\Users.csv";

        public static void RemoveBookingFromFile(int id)
        {
            List<string> lines = new List<string>();

            lines.AddRange(File.ReadAllLines(filePath));
            string headers = lines[0];
            lines.RemoveAt(0);
            lines = lines.Where(line =>
            line.Split(',').Length < 7 ||
            int.Parse(line.Split(',')[6]) != id)
                .ToList();
            List<string> newLines = new List<string>();
            newLines.Add(headers);
            newLines.AddRange(lines);
            File.WriteAllLines(filePath, lines);
        }
    }
}
