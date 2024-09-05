using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Airport;
using TicketBooking.Class;
using TicketBooking.Country;
using TicketBooking.Flight.FlightModel;

namespace TicketBooking.User.Passenger.Bookings;

public class BookingsService
{
    public List<BookingsModel> Bookings { get; init; }

    public void AddBooking(Flight.FlightModel.Flight flight, ClassEnum flightClass, int userId)
    {
        BookingsModel booking = new BookingsModel()
        {
            Id = (int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
            UserId = userId,
            Flight = flight,
            ChosenClass = flightClass
        };

        Bookings.Add(booking);
        BookingsRepository.AddBookingToFile(booking);
    }

    public void CancelBooking(int id)
    {
        BookingsRepository.RemoveBookingFromFile(id);
        BookingsModel? toBeRemoved = Bookings.SingleOrDefault(booking => booking.Id == id);
        if (toBeRemoved is not null)
        {
            Bookings.Remove(toBeRemoved);
        }
    }

    public void UpdateBooking(int id, ClassEnum newClass)
    {
        // Here I'm intended to get exception raised in case of error or no item holding this Id
        BookingsModel? toBeModified = Bookings.Find(booking => booking.Id == id);
        toBeModified.ChosenClass = newClass;
        BookingsRepository.UpdateBooking(toBeModified);
    }

    public void DisplayBookings(bool displayUserId = false)
    {
        foreach (var booking in Bookings)
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
        ClassEnum? flightClass = null
        )
    {
        List<BookingsModel> tempBookings = Bookings;

        if (passengerId != null)
        {
            tempBookings = tempBookings.Where(booking => booking.UserId == passengerId).ToList();
        }

        if (flightId != null)
        {
            tempBookings = tempBookings.Where(booking => booking.Flight.Id == flightId).ToList();
        }

        if (departureCountry is not null)
        {
            tempBookings = tempBookings
                .Where(
                booking =>
                booking.Flight.DepartureCountry
                .Equals((CountryEnum)Enum.Parse(typeof(CountryEnum), departureCountry))).ToList();
        }

        if (destinationCountry is not null)
        {
            tempBookings = tempBookings.Where(booking => booking.Flight.DestinationCountry.Equals((CountryEnum)Enum.Parse(typeof(CountryEnum), destinationCountry))).ToList();
        }

        if (departureAirport is not null)
        {
            tempBookings = tempBookings.Where(booking => booking.Flight.DepartureAirport.Equals((AirportEnum)Enum.Parse(typeof(AirportEnum), departureAirport))).ToList();
        }

        if (arrivalAirport is not null)
        {
            tempBookings = tempBookings.Where(booking => booking.Flight.ArrivalAirport.Equals((AirportEnum)Enum.Parse(typeof(AirportEnum), arrivalAirport))).ToList();
        }

        if (flightClass is not null)
        {
            tempBookings = tempBookings
                .Where(booking => booking.ChosenClass.Equals(flightClass))
                .ToList();
        }

        if (priceFrom is not null)
        {
            tempBookings = tempBookings.Where(booking => booking.Flight.Class[booking.ChosenClass] >= priceFrom).ToList();
        }

        if (priceTo is not null)
        {
            tempBookings = tempBookings.Where(booking => booking.Flight.Class[booking.ChosenClass] <= priceFrom).ToList();
        }

        return tempBookings;
    }
}