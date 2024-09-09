using TicketBooking.Classes;
using TicketBooking.AppSettings;
using TicketBooking.Users.Passengers.Bookings;
using TicketBooking.Users.Managers.Models;
using TicketBooking.Users.Passengers.Models;
using TicketBooking.Flights.Services;
using TicketBooking.FileProcessor.CSVProcessor;

namespace TicketBooking.Users.Repository;

public class UserRepository
{
    public List<UserModel> Users { get; set; } = new List<UserModel>();
    public FlightServices _flight = new FlightServices();
    private static UserRepository _instance = new UserRepository();

    private UserRepository()
    {
        LoadUsers();
    }

    public static UserRepository GetInstance()
    {
        _instance ??= new();
        return _instance;
    }

    public List<UserModel> GetUsers() => Users;

    private void LoadUsers()
    {
        string path = AppSettingsInitializer.AppSettingsInstance().UsersRepoPath;
        var lines = CSVProcessor.Load(path);
        List<BookingsModel> passengerBookings = new List<BookingsModel>();

        foreach (var values in lines)
        {
            string role = values[0];
            UserModel user = new UserModel();

            if (role.Equals("passenger", StringComparison.OrdinalIgnoreCase))
            {
                if (passengerBookings.Count > 0)
                {
                    ((PassengerModel)Users[Users.Count - 1]).PersonalFlights = new List<BookingsModel>(passengerBookings);
                    passengerBookings.Clear();
                }

                user = new PassengerModel()
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
                    ChosenClass = (Class)Enum.Parse(typeof(Class), values[8]),
                    UserId = Users.Last().Id
                });
            }
        }
    }

    public UserModel Login(string email, string password)
    {
        return Users.FirstOrDefault(user => user.Email.Equals(email) && user.Password.Equals(password));
    }
}
