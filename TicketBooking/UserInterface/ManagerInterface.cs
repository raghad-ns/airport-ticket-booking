using TicketBooking.AppSettings;
using TicketBooking.Classes;
using TicketBooking.Flights.Models;
using TicketBooking.Flights.Repository;
using TicketBooking.Flights.Services;
using TicketBooking.Users;
using TicketBooking.Users.Passengers.Bookings;
using TicketBooking.Users.Passengers.Models;

namespace TicketBooking.UserInterface;

public class ManagerInterface
{
    private readonly FlightServices _flightServices;
    private readonly List<BookingsModel> _bookings = new List<BookingsModel>();
    BookingsService _bookingsService;

    public ManagerInterface(List<UserModel> users, List<Flight> flights)
    {
        _flightServices = new(flights, FlightRepository.GetInstance(flights), new FlightPrinter());

        _bookings.AddRange(
            users.OfType<PassengerModel>()
            .SelectMany(user => user.PersonalFlights)
            );

        _bookingsService = new(_bookings, new BookingsRepository());
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
        Console.WriteLine("Users' bookings: ");
        _bookingsService.DisplayBookings(displayUserId: true);
    }

    public void UploadFlights()
    {
        string? path = AppSettingsInitializer.AppSettingsInstance().FlightsRepoPath;
        Console.WriteLine("Please enter the absolute path of the file contains flights' details: ");
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
        Class? flightClass = string.IsNullOrWhiteSpace(userInput) ? null : (Class)Enum.Parse(typeof(Class), userInput);

        Console.WriteLine("Matched flights: ");

        var filteredBookings = _bookingsService.FilterBookings(
                passengerId,
                flightId,
                priceFrom,
                priceTo,
                departureCountry,
                destinationCountry,
                departureAirport,
                arrivalAirport,
                flightClass);

        foreach (var booking in filteredBookings)
        {
            Console.WriteLine($"Booking Id: {booking.Id}");
            Console.WriteLine($"User Id: {booking.UserId}");
            Console.WriteLine(booking.Flight.ToString());
            Console.WriteLine($"Class: {booking.ChosenClass}");
        }
    }
}