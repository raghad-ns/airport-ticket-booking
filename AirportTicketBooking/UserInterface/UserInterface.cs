using AirportTicketBooking.Flight.FlightRepository;
using AirportTicketBooking.User;
using AirportTicketBooking.User.Manager.ManagerModel;
using AirportTicketBooking.User.Passenger.PassengerModel;
using AirportTicketBooking.User.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking.UserInterface
{
    public class UserInterface
    {
        private UserModel _user;
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
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
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
                        string path = "C:\\Users\\M.T\\Desktop\\projects\\foothill\\practice-projects\\AirportTicketBooking\\AirportTicketBooking\\assets\\managerFlightsSample.csv";
                        Console.WriteLine("Please enter the absolute path of the file contains flights' details: ");
                        path = Console.ReadLine();
                        Console.WriteLine(path);
                        path = path.Replace("\\", "\\\\");
                        Console.WriteLine(path);
                        try
                        {
                            _flights.UploadFlights(path);
                            Console.WriteLine("Flights uploaded successfully!");
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

        public void DisplayFlights()
        {
            Console.WriteLine($"Available flights: ");
            foreach (var flight in _flights.Flights)
            {
                Console.WriteLine(flight.ToString());
            }
        }
    }
}
