using TicketBooking.AppSettings;
using TicketBooking.Classes;
using TicketBooking.FileProcessor.Deserializer.User;
using TicketBooking.Flights.Models;
using TicketBooking.Users.Managers.Models;
using TicketBooking.Users.Passengers.Bookings;
using TicketBooking.Users.Passengers.Models;
using TicketBooking.Users;
using TicketBooking.Flights.Services;
using TicketBooking.FileProcessor.Deserializer.Flight;

namespace TicketBooking.FileProcessor.Services;

internal class InitApplicationService
{
    public List<UserModel> Users { get; set; } = new ();
    public List<Flight> _flights = new ();

    public List<UserModel> LoadUsers()
    {
        string path = AppSettingsInitializer.AppSettingsInstance().UsersRepoPath;
        var lines = CSVProcessor.CSVProcessor.Load(path);
        List<BookingsModel> passengerBookings = new List<BookingsModel>();

        foreach (var values in lines)
        {
            UserModel user = new UserModel();
            (
                string? role,
                int id,
                string? firstName,
                string? lastName,
                string? email,
                string? password,
                int? bookingId,
                int? flightId,
                string? flightClass
                ) = UserDeserializer.Deserialize(values);

            if (role.Equals("passenger", StringComparison.OrdinalIgnoreCase))
            {
                if (passengerBookings.Count > 0)
                {
                    ((PassengerModel)Users[Users.Count - 1]).PersonalFlights = new List<BookingsModel>(passengerBookings);
                    passengerBookings.Clear();
                }

                user = new PassengerModel()
                {
                    Id = id,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password,
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
                    Id = id,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password
                };

                Users.Add(user);
            }
            else
            {
                passengerBookings.Add(new BookingsModel()
                {
                    Id = bookingId ?? 0,
                    Flight = _flights.Single(flight => flight.Id == flightId),
                    ChosenClass = (Class)Enum.Parse(typeof(Class), flightClass),
                    UserId = Users.Last().Id
                });
            }
        }
        return Users;
    }

    public List<Flight> LoadFlights()
    {
        string path = AppSettingsInitializer.AppSettingsInstance().FlightsRepoPath;
        var lines = CSVProcessor.CSVProcessor.Load(path);

        foreach (var values in lines)
        {
            var DeserializedFlightData = FlightDeserializer.Deserialize(values);
            _flights.Add(FlightServices.GetFlightObject(DeserializedFlightData));
        }
        return _flights;
    }
}