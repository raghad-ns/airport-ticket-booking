﻿using System;
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

        public static void AddBookingToFile(BookingsModel booking)
        {
            List<string> lines = new List<string>();
            lines.AddRange(File.ReadAllLines(filePath));
            string headers = lines[0];
            lines.RemoveAt(0);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
            string newLine = ",,,,,," + booking.Id + "," + booking.Flight.Id + "," + booking.ChosenClass.ToString();
            int insertIndex = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                if (int.Parse(lines[i].Split(',')[1]) == booking.UserId)
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

            lines.Insert(0,headers);
            File.WriteAllLines(filePath, lines);
        }
    }
}
