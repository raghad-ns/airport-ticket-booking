using TicketBooking.Airports;
using TicketBooking.Classes;
using TicketBooking.Countries;

namespace TicketBooking.Users.Passengers.Bookings;

public class BookingsService
{
    private List<BookingsModel> _bookings { get; init; }
    public BookingsService(List<BookingsModel> bookings)
    {
        _bookings = bookings;
    }

    public List<BookingsModel> BetBookings() { return _bookings; }

    public void AddBooking(Flights.Models.Flight flight, Class flightClass, int userId)
    {
        BookingsModel booking = new BookingsModel()
        {
            Id = (int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
            UserId = userId,
            Flight = flight,
            ChosenClass = flightClass
        };

        _bookings.Add(booking);
        BookingsRepository.AddBookingToFile(booking);
    }

    public void CancelBooking(int id)
    {
        BookingsRepository.RemoveBookingFromFile(id);
        BookingsModel? toBeRemoved = _bookings.SingleOrDefault(booking => booking.Id == id);

        if (toBeRemoved is not null)
        {
            _bookings.Remove(toBeRemoved);
        }
    }

    public void UpdateBooking(int id, Class newClass)
    {
        // Here I'm intended to get exception raised in case of error or no item holding this Id
        BookingsModel? toBeModified = _bookings.Find(booking => booking.Id == id);
        toBeModified.ChosenClass = newClass;
        BookingsRepository.UpdateBooking(toBeModified);
    }

    public void DisplayBookings(bool displayUserId = false)
    {
        foreach (var booking in _bookings)
        {
            if (displayUserId) Console.WriteLine($"User Id: {booking.UserId}");

            Console.WriteLine(booking.ToString());
        }
    }

    public List<BookingsModel> FilterBookings(
        int? passengerId = null,
        int? flightId = null,
        double? priceFrom = null,
        double? priceTo = null,
        string? departureCountry = null,
        string? destinationCountry = null,
        string? departureAirport = null,
        string? arrivalAirport = null,
        Class? flightClass = null
        )
    {
        IEnumerable<BookingsModel> tempBookings = _bookings;

        if (passengerId != null)
        {
            tempBookings = tempBookings.Where(booking => booking.UserId == passengerId);
        }

        if (flightId != null)
        {
            tempBookings = tempBookings.Where(booking => booking.Flight.Id == flightId);
        }

        if (departureCountry is not null)
        {
            tempBookings = tempBookings
                .Where(
                booking =>
                booking.Flight.DepartureCountry
                .Equals((Country)Enum.Parse(typeof(Country), departureCountry)));
        }

        if (destinationCountry is not null)
        {
            tempBookings = tempBookings.Where(booking => booking.Flight.DestinationCountry.Equals((Country)Enum.Parse(typeof(Country), destinationCountry)));
        }

        if (departureAirport is not null)
        {
            tempBookings = tempBookings.Where(booking => booking.Flight.DepartureAirport.Equals((Airport)Enum.Parse(typeof(Airport), departureAirport)));
        }

        if (arrivalAirport is not null)
        {
            tempBookings = tempBookings.Where(booking => booking.Flight.ArrivalAirport.Equals((Airport)Enum.Parse(typeof(Airport), arrivalAirport)));
        }

        if (flightClass is not null)
        {
            tempBookings = tempBookings
                .Where(booking => booking.ChosenClass.Equals(flightClass));
        }

        if (priceFrom is not null)
        {
            tempBookings = tempBookings.Where(booking => booking.Flight.Class[booking.ChosenClass] >= priceFrom);
        }

        if (priceTo is not null)
        {
            tempBookings = tempBookings.Where(booking => booking.Flight.Class[booking.ChosenClass] <= priceFrom);
        }

        return tempBookings.ToList();
    }
}