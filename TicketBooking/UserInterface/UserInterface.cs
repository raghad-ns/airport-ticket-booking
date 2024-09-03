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

namespace TicketBooking.UserInterface
{
    public class UserInterface
    {
        private UserModel? _user;
        private UserRepository _userRepository = new UserRepository();
        private FlightRepository _flights = new FlightRepository();
        public void ShowInitialMenu()
        {
            if (_user == null)
            {
                Console.WriteLine("You have to login to be able to use the system!");
                ShowLoginMenu();
            }
            else if (_user is PassengerModel)
            {
                ShowPassengerOptions();
            }
            else
            {
                ShowManagerOptions();
            }
        }

        public void ShowLoginMenu()
        {
            Console.Write("Email: ");
            string? email = Console.ReadLine();
            Console.Write("Password: ");
            string? password = Console.ReadLine();
            _user = _userRepository.Login(email, password);
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
                    ShowPassengerOptions();
                }
                else if (_user is ManagerModel)
                {
                    ShowManagerOptions();
                }
            }
        }

        public void ShowPassengerOptions()
        {
            Console.WriteLine("Hello passenger!");
            Console.WriteLine("1. Book a flight.");
            Console.WriteLine("2. Search for specific flights.");
            Console.WriteLine("3. Manage bookings.");
            Console.WriteLine("0. Exit.");
            Console.WriteLine("Choose one option: ");
            int option = int.Parse(Console.ReadLine());

            const int bookFlight = 1;
            const int searchFlight = 2;
            const int manageBookings = 3;

            while (option > 0)
            {
                switch (option)
                {
                    case bookFlight:
                        DisplayFlights();
                        break;
                    case searchFlight:
                        SearchAvialableFlight();
                        break;
                    case manageBookings:
                        break;
                    default:
                        break;

                }
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Hello passenger!");
                Console.WriteLine("1. Book a flight.");
                Console.WriteLine("2. Search for specific flights.");
                Console.WriteLine("3. Manage bookings.");
                Console.WriteLine("0. Exit.");
                Console.WriteLine("Choose one option: ");
                option = int.Parse(Console.ReadLine());
            }
        }

        public void ShowManagerOptions()
        {
            Console.WriteLine("Hello manager!");
            Console.WriteLine("1. Display available flights.");
            Console.WriteLine("2. Search for specific flights.");
            Console.WriteLine("3. Upload new flights' details.");
            Console.WriteLine("0. Exit.");
            Console.WriteLine("Choose one option: ");
            int option = int.Parse(Console.ReadLine());

            const int displayAvailableFlights = 1;
            const int searchFlight = 2;
            const int uploadFlights = 3;

            while (option > 0)
            {
                switch (option)
                {
                    case displayAvailableFlights:
                        DisplayFlights();
                        break;
                    case searchFlight:
                        Console.WriteLine("search");
                        break;
                    case uploadFlights:
                        string path = "C:\\Users\\M.T\\Desktop\\projects\\foothill\\practice-projects\\AirportTicketBooking\\TicketBooking\\Flight\\FlightRepository\\Flights.csv";
                        Console.WriteLine("Please enter the absolute path of the file contains flights' details: ");
                        path = Console.ReadLine();
                        Console.WriteLine(path);
                        path = path.Replace("\\", "\\\\");
                        Console.WriteLine(path);
                        try
                        {
                            _flights.UploadFlights(path);
                        }
                        catch (Exception ex) { Console.WriteLine("Cannot upload data, please try again later!"); }
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;

                }
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Hello manager!");
                Console.WriteLine("1. Display available flights.");
                Console.WriteLine("2. Search for specific flights.");
                Console.WriteLine("3. Upload new flights' details.");
                Console.WriteLine("0. Exit.");
                Console.WriteLine("Choose one option: ");
                option = int.Parse(Console.ReadLine());
            }
        }

        public void ShowBookingsManagementOptions()
        {
            Console.WriteLine("1. Display personal bookings.");
            Console.WriteLine("2. Modify a booking.");
            Console.WriteLine("3. Cancel a booking.");
            Console.WriteLine("0. Back.");
            Console.WriteLine("Choose one option: ");
            int option = int.Parse(Console.ReadLine());

            const int displayBookings = 1;
            const int modifyBooking = 2;
            const int cancelBooking = 3;

            while (option > 0)
            {
                switch (option)
                {
                    case displayBookings:
                        DisplayFlights();
                        break;
                    case modifyBooking:
                        SearchAvialableFlight();
                        break;
                    case cancelBooking:
                        break;
                    default:
                        break;

                }
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("1. Display personal bookings.");
                Console.WriteLine("2. Modify a booking.");
                Console.WriteLine("3. Cancel a booking.");
                Console.WriteLine("0. Back.");
                Console.WriteLine("Choose one option: ");
                option = int.Parse(Console.ReadLine());
            }
        }

        public void DisplayFlights()
        {
            Console.WriteLine($"Available flights: ");
            foreach (var flight in _flights.FilterFlights())
            {
                Console.WriteLine(flight.ToString());
            }
        }

        public void SearchAvialableFlight()
        {
            string userInput;

            Console.WriteLine("Please fill the filters");
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

            Console.WriteLine("Class: 1. First class, 2. Business class, 3. Economy");
            userInput = Console.ReadLine();
            ClassEnum? flightClass = string.IsNullOrWhiteSpace(userInput) ? null : (ClassEnum)int.Parse(userInput);

            Console.WriteLine("Matched flights: ");
            foreach (var flight in
                _flights.FilterFlights(priceFrom, priceTo, departureCountry, destinationCountry, departureAirport, arrivalAirport, flightClass)
                )
            {
                Console.WriteLine(flight.GetFlightDetails());
            }
        }
    }
}
