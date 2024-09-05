using TicketBooking.Airport;
using TicketBooking.Class;
using TicketBooking.Country;
using TicketBooking.Flight.FlightModel;
using TicketBooking.Flight.FlightRepository;
using TicketBooking.User;
using TicketBooking.User.Manager.ManagerModel;
using TicketBooking.User.Passenger.PassengerModel;
using TicketBooking.User.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.User.Passenger.Bookings;
using TicketBooking.Flight.Services;
using TicketBooking.User.UserServices;

namespace TicketBooking.UserInterface;

public class UserInterface
{
    private UserModel? _user;
    private UserServices _userServices = new ();
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
            PassengerInterface passengerInterface = new PassengerInterface()
            {
                Passenger = passenger,
                Bookings = passenger.PersonalFlights,
                BookingsService = new BookingsService(passenger.PersonalFlights)
            };

            passengerInterface.ShowPassengerOptions();
        }
        else
        {
            ManagerInterface managerInterface = new ManagerInterface();
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
                PassengerInterface passengerInterface = new PassengerInterface()
                {
                    Passenger = passenger,
                    Bookings = passenger.PersonalFlights,
                    BookingsService = new BookingsService(passenger.PersonalFlights)
                };
                passengerInterface.ShowPassengerOptions();
            }
            else if (_user is ManagerModel)
            {
                _user = (ManagerModel)_user;
                ManagerInterface managerInterface = new ManagerInterface();
                managerInterface.ShowManagerOptions();
            }
        }
    }
}