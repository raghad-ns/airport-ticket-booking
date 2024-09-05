using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.AppSettings;
using TicketBooking.Class;
using TicketBooking.Flight.FlightModel;
using TicketBooking.Flight.FlightRepository;
using TicketBooking.Flight.Services;
using TicketBooking.User.Passenger.Bookings;
using TicketBooking.User.Passenger.PassengerModel;
using TicketBooking.User.UserRepository;

namespace TicketBooking.UserInterface;

public class ManagerInterface
{
    private readonly FlightServices _flightServices = new ();
    private readonly List<BookingsModel> _bookings = new List<BookingsModel>();

    public ManagerInterface()
    {
        UserRepository users = new UserRepository();

        foreach (var user in users.Users)
        {
            if (user is PassengerModel)
            {
                _bookings.AddRange(((PassengerModel)user).PersonalFlights);
            }
        }

    }
    public void ShowManagerOptions()
    {

        const int displayAvailableFlights = 1;
        const int displayBookings = 2;
        const int searchBooking = 3;
        const int uploadFlights = 4;
        int option = 1;

        while (option > 0)
        {
            Console.WriteLine("Hello manager!");
            Console.WriteLine("1. Display available flights.");
            Console.WriteLine("2. Display user's bookings.");
            Console.WriteLine("3. Search users' bookings.");
            Console.WriteLine("4. Upload new flights' details.");
            Console.WriteLine("0. Exit.");
            Console.WriteLine("Choose one option: ");
            option = int.Parse(Console.ReadLine() ?? "0");
            switch (option)
            {
                case displayAvailableFlights:
                    _flightServices.DisplayFlights();
                    break;
                case displayBookings:
                    DisplayAllBookings();
                    break;
                case searchBooking:
                    SearchBooking();
                    break;
                case uploadFlights:
                    UploadFlights();
                    break;
                default:
                    break;

            }
            Console.ReadLine();
            Console.Clear();
        }
    }

    public void DisplayAllBookings()
    {
        BookingsService bookingsService = new BookingsService(_bookings);
        Console.WriteLine("Users' bookings: ");
        bookingsService.DisplayBookings(displayUserId: true);
    }

    public void UploadFlights()
    {
        string? path = AppSettingsInitializer.AppSettingsInstance.FlightsRepoPath;
        Console.WriteLine("Please enter the absolute path of the file contains flights' details: ");
        Console.WriteLine(Directory.GetCurrentDirectory());
        path = Console.ReadLine();

        try
        {
            path = path?.Replace("\\", "\\\\");
            _flightServices.LoadFlights(path);
        }
        catch (Exception ex) { Console.WriteLine("Cannot upload data, please try again later!"); }
    }

    public void SearchBooking()
    {
        string? userInput;
        Console.WriteLine("Please fill the filters");

        Console.Write("Passenger Id: ");
        userInput = Console.ReadLine();
        int? passengerId = string.IsNullOrWhiteSpace(userInput) ? null : int.Parse(userInput);

        Console.Write("Flight Id: ");
        userInput = Console.ReadLine();
        int? flightId = string.IsNullOrWhiteSpace(userInput) ? null : int.Parse(userInput);

        Console.Write("Price, greater than: ");
        userInput = Console.ReadLine();
        double? priceFrom = string.IsNullOrWhiteSpace(userInput) ? null : double.Parse(userInput);

        Console.Write("Price, less than: ");
        userInput = Console.ReadLine();
        double? priceTo = string.IsNullOrWhiteSpace(userInput) ? null : double.Parse(userInput);

        Console.Write("Departure country: ");
        userInput = Console.ReadLine();
        string? departureCountry = string.IsNullOrWhiteSpace(userInput) ? null : userInput;

        Console.Write("Destination country: ");
        userInput = Console.ReadLine();
        string? destinationCountry = string.IsNullOrWhiteSpace(userInput) ? null : userInput;

        Console.Write("Departure airport: ");
        userInput = Console.ReadLine();
        string? departureAirport = string.IsNullOrWhiteSpace(userInput) ? null : userInput;

        Console.Write("Arrival airport: ");
        userInput = Console.ReadLine();
        string? arrivalAirport = string.IsNullOrWhiteSpace(userInput) ? null : userInput;

        Console.WriteLine("Class: FirstClass, Business, Economy?");
        userInput = Console.ReadLine();
        ClassEnum? flightClass = string.IsNullOrWhiteSpace(userInput) ? null : (ClassEnum)Enum.Parse(typeof(ClassEnum), userInput);

        BookingsService bookingsService = new BookingsService(_bookings);
        Console.WriteLine("Matched flights: ");

        foreach (var booking in
            bookingsService.FilterBookings(
                passengerId,
                flightId,
                priceFrom,
                priceTo,
                departureCountry,
                destinationCountry,
                departureAirport,
                arrivalAirport,
                flightClass)
            )
        {
            Console.WriteLine($"Booking Id: {booking.Id}");
            Console.WriteLine($"User Id: {booking.UserId}");
            Console.WriteLine(booking.Flight.ToString());
            Console.WriteLine($"Class: {booking.ChosenClass}");
        }
    }
}