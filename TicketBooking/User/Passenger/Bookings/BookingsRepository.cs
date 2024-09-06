using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.AppSettings;
using TicketBooking.Class;

namespace TicketBooking.User.Passenger.Bookings;

public class BookingsRepository
{
    //public List<BookingsModel> Bookings { get; set; }
    private static string filePath = AppSettingsInitializer.AppSettingsInstance.UsersRepoPath;

    public static void AddBookingToFile(BookingsModel booking)
    {
        List<string> lines = new List<string>();

        lines.AddRange(File.ReadAllLines(filePath));
        string headers = lines[0];
        lines.RemoveAt(0);

        string newLine = ",,,,,," + booking.Id + "," + booking.Flight.Id + "," + booking.ChosenClass.ToString();
        int insertIndex = 0;

        for (int i = 0; i < lines.Count; i++)
        {
            string id = string.IsNullOrWhiteSpace(lines[i].Split(',')[1]) ? "-1" : lines[i].Split(',')[1];

            if (int.Parse(id) == booking.UserId)
            {
                insertIndex = i + 1;
                break;
            }
        }

        lines.Insert(insertIndex, newLine);
        lines.Insert(0, headers);

        File.WriteAllLines(filePath, lines);
    }

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

        lines.Insert(0, headers);
        File.WriteAllLines(filePath, lines);
    }

    public static void UpdateBooking(BookingsModel newBooking)
    {
        List<string> lines = new List<string>();

        lines.AddRange(File.ReadAllLines(filePath));
        string headers = lines[0];
        lines.RemoveAt(0);

        int index = lines.FindIndex(line => line.Split(',').Length > 6 && int.Parse(line.Split(",")[6]) == newBooking.Id);
        string[] values = lines[index].Split(",");
        values[8] = Enum.GetName(typeof(ClassEnum), newBooking.ChosenClass);
        lines[index] = string.Join(",", values);

        lines.Insert(0, headers);

        File.WriteAllLines(filePath, lines);
    }
}