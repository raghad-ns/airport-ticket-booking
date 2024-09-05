using TicketBooking.UserInterface;

namespace TicketBooking;

internal class Program
{
    static void Main(string[] args)
    {
        UserInterface.UserInterface userInterface = new UserInterface.UserInterface();
        userInterface.ShowInitialMenu();
    }
}
