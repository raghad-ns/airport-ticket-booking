using TicketBooking.Airport;
using TicketBooking.Class;
using TicketBooking.Country;
using TicketBooking.Flight.FlightModel;
using TicketBooking.User.Manager.ManagerModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.User.Passenger.Bookings;
using TicketBooking.User.Passenger.PassengerModel;
using TicketBooking.Flight.FlightRepository;
using TicketBooking.AppSettings;

namespace TicketBooking.User.UserRepository;

public class UserRepository
{
    public List<UserModel> Users { get; set; } = new List<UserModel>();
    public FlightRepository _flight = new FlightRepository();

    public UserRepository()
    {
        LoadUsers();
    }

    private void LoadUsers()
    {
        string path = AppSettingsInitializer.AppSettingsInstance.UsersRepoPath;

        using (var reader = new StreamReader(path))
        {
            var headerLine = reader.ReadLine();
            List<BookingsModel> passengerBookings = new List<BookingsModel>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                string role = values[0];
                UserModel user = new UserModel();

                if (role.Equals("passenger", StringComparison.OrdinalIgnoreCase))
                {
                    if (passengerBookings.Count > 0)
                    {
                        ((PassengerModel)Users[Users.Count - 1]).PersonalFlights = new List<BookingsModel>(passengerBookings);
                        passengerBookings.Clear();
                    }

                    user = new Passenger.PassengerModel.PassengerModel()
                    {
                        Id = int.Parse(values[1]),
                        FirstName = values[2],
                        LastName = values[3],
                        Email = values[4],
                        Password = values[5],
                        PersonalFlights = new()
                    };

                    Users.Add(user);
                }
                else if (role.Equals("manager", StringComparison.OrdinalIgnoreCase))
                {
                    if (passengerBookings.Count > 0)
                    {
                        ((PassengerModel)Users[Users.Count - 1]).PersonalFlights = new List<BookingsModel>(passengerBookings);
                        passengerBookings.Clear();
                    }

                    user = new ManagerModel()
                    {
                        Id = int.Parse(values[1]),
                        FirstName = values[2],
                        LastName = values[3],
                        Email = values[4],
                        Password = values[5]
                    };

                    Users.Add(user);
                }
                else
                {
                    passengerBookings.Add(new BookingsModel()
                    {
                        Id = int.Parse(values[6]),
                        Flight = _flight.GetFlights().Single(flight => flight.Id == int.Parse(values[7])),
                        ChosenClass = (ClassEnum)Enum.Parse(typeof(ClassEnum), values[8]),
                        UserId = Users.Last().Id
                    });
                }
            }
        }
    }

    public UserModel Login(string email, string password)
    {
        return Users.FirstOrDefault(user => user.Email.Equals(email) && user.Password.Equals(password));
    }
}
