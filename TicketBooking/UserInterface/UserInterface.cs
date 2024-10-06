using TicketBooking.Users.Passengers.Bookings;
using TicketBooking.Users;
using TicketBooking.Users.Managers.Models;
using TicketBooking.Users.Passengers.Models;
using TicketBooking.Users.Services;
using TicketBooking.Flights.Models;

namespace TicketBooking.UserInterface;

public class UserInterface
{
    private List<UserModel> _users;
    private List<Flight> _flights;
    private UserModel? _user;
    private UserServices _userServices ;

    public UserInterface(List<UserModel> users, List<Flight> flights)
    {
        _users = users;
        _flights = flights;
        _userServices = new(users);
    }
    public void ShowInitialMenu()
    {
        if (_user == null)
        {
            Console.WriteLine("You have to login to be able to use the system!");
            ShowLoginMenu();
        }
        else if (_user is PassengerModel)
        {
            PassengerModel passenger = (PassengerModel)_user;
            PassengerInterface passengerInterface = new PassengerInterface(
                passenger: passenger,
                bookingsServices: new BookingsService(passenger.PersonalFlights),
                flightServices: new Flights.Services.FlightServices(_flights)
                );

            passengerInterface.ShowPassengerOptions();
        }
        else
        {
            ManagerInterface managerInterface = new ManagerInterface(_users, _flights);
            managerInterface.ShowManagerOptions();
        }
    }

    public void ShowLoginMenu()
    {
        Console.Write("Email: ");
        string? email = Console.ReadLine();

        Console.Write("Password: ");
        string? password = Console.ReadLine();

        _user = _userServices.Login(email ?? string.Empty, password ?? string.Empty);

        if (_user is null)
        {
            Console.WriteLine("Incorrect login credentials, please try again");
            Console.ReadLine();
            Console.Clear();
            ShowLoginMenu();
        }
        else
        {
            Console.WriteLine($"logged in successfully, {_user.FirstName}");
            Console.ReadLine();
            Console.Clear();

            if (_user is PassengerModel)
            {
                PassengerModel passenger = (PassengerModel)_user;

                PassengerInterface passengerInterface = new PassengerInterface(
                    passenger: passenger,
                    bookingsServices: new BookingsService(passenger.PersonalFlights),
                    flightServices: new Flights.Services.FlightServices(_flights)
                );

                passengerInterface.ShowPassengerOptions();
            }
            else if (_user is ManagerModel)
            {
                _user = (ManagerModel)_user;
                ManagerInterface managerInterface = new ManagerInterface(_users, _flights);
                managerInterface.ShowManagerOptions();
            }
        }
    }
}