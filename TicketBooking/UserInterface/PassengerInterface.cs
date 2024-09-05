using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Class;
using TicketBooking.Flight.FlightModel;
using TicketBooking.Flight.FlightRepository;
using TicketBooking.User.Passenger.Bookings;
using TicketBooking.User.Passenger.PassengerModel;

namespace TicketBooking.UserInterface
{
    public class PassengerInterface
    {
        public PassengerModel Passenger { get; init; }
        public List<BookingsModel> Bookings { get; init; }
        public BookingsService BookingsService { get; init; }
        public FlightRepository _flights = new FlightRepository();

        public void ShowPassengerOptions()
        {
            Console.WriteLine("Hello passenger!");
            Console.WriteLine("1. Book a flight.");
            Console.WriteLine("2. Search for specific flights.");
            Console.WriteLine("3. Manage bookings.");
            Console.WriteLine("0. Exit.");
            Console.WriteLine("Choose one option: ");
            int option = int.Parse(Console.ReadLine() ?? "0");

            const int bookFlight = 1;
            const int searchFlight = 2;
            const int manageBookings = 3;

            while (option > 0)
            {
                switch (option)
                {
                    case bookFlight:
                        BookFlight();
                        break;
                    case searchFlight:
                        SearchAvailableFlight();
                        break;
                    case manageBookings:
                        ShowBookingsManagementOptions();
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
                option = int.Parse(Console.ReadLine() ?? "0");
            }
        }

        public void ShowBookingsManagementOptions()
        {
            Console.WriteLine("1. Display personal bookings.");
            Console.WriteLine("2. Modify a booking.");
            Console.WriteLine("3. Cancel a booking.");
            Console.WriteLine("0. Back.");
            Console.WriteLine("Choose one option: ");
            int option = int.Parse(Console.ReadLine() ?? "0");

            const int displayBookings = 1;
            const int modifyBooking = 2;
            const int cancelBooking = 3;

            while (option > 0)
            {
                switch (option)
                {
                    case displayBookings:
                        Console.WriteLine("Personal bookings: ");
                        BookingsService.DisplayBookings();
                        break;
                    case modifyBooking:
                        ModifyBookings();
                        break;
                    case cancelBooking:
                        CancelBooking();
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
                option = int.Parse(Console.ReadLine() ?? "0");
            }
        }

        public void BookFlight()
        {
            _flights.DisplayFlights();
            Console.WriteLine("Are you looking for specific flight? enter 'y' if yes");
            string choice = Console.ReadLine() ?? string.Empty;
            if (choice.Equals("y", StringComparison.OrdinalIgnoreCase) || choice.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                SearchAvailableFlight();
            }
            Console.WriteLine("Enter the flight Id to book it: ");
            int flightToBookId = int.Parse(Console.ReadLine() ?? string.Empty);
            try
            {
                Flight.FlightModel.Flight flightToBook = _flights.Flights.Single(flight => flight.Id == flightToBookId);
                Console.WriteLine(flightToBook.GetFlightClassesAndPrices());
                Console.WriteLine("Please choose the suitable class: ");
                string chosenClass = Console.ReadLine() ?? string.Empty;
                BookingsService bookingsService = new BookingsService() { Bookings = Passenger.PersonalFlights };
                bookingsService.AddBooking(flightToBook, (ClassEnum)Enum.Parse(typeof(ClassEnum), chosenClass), Passenger.Id);
                Console.WriteLine("Chosen flight booked successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not book this flight, please try another one, or try again later!");
            }
        }

        // Modification done by upgrading/downgrading flight class
        public void ModifyBookings()
        {
            Console.WriteLine("Personal bookings: ");
            BookingsService.DisplayBookings();
            Console.WriteLine("Please enter the id of the flight to be modified: ");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine(Bookings.Single(booking => booking.Id == id).Flight.GetFlightClassesAndPrices());
                Console.WriteLine("Choose your new class: ");
                string newClass = Console.ReadLine() ?? string.Empty;
                if (Enum.IsDefined(typeof(ClassEnum), newClass))
                {
                    BookingsService.UpdateBooking(id, (ClassEnum)(Enum.Parse(typeof(ClassEnum), newClass)));
                    Console.WriteLine("Flight modified successfully!");
                }
                else
                {
                    Console.WriteLine("Incorrect class!");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Cannot modify the selected flight, please check the id you entered!");
            }
        }

        public void CancelBooking()
        {
            Console.WriteLine("Personal bookings: ");
            BookingsService.DisplayBookings();
            Console.WriteLine("Enter the id to cancel: ");
            try
            {
                int id = int.Parse(Console.ReadLine() ?? string.Empty);
                BookingsService.CancelBooking(id);
                Console.WriteLine("Flight canceled successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot cancel the chosen flight, please check the id you entered!");
            }
        }


        public void SearchAvailableFlight()
        {
            string? userInput;

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
