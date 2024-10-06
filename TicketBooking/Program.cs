using TicketBooking.FileProcessor.Services;
using TicketBooking.Flights.Models;
using TicketBooking.Users;

namespace TicketBooking;

internal class Program
{
    private static List<UserModel> _users;
    private static List<Flight> _flights;

    static void Main(string[] args)
    {
        InitApp();

        UserInterface.UserInterface userInterface = new UserInterface.UserInterface(_users, _flights);
        userInterface.ShowInitialMenu();
    }

    private static void InitApp()
    {
        InitApplicationService initApplicationService = new InitApplicationService();
        _flights = initApplicationService.LoadFlights();
        _users = initApplicationService.LoadUsers();
    }
}