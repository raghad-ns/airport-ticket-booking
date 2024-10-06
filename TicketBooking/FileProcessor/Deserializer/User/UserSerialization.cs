using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.FileProcessor.Deserializer.User;

public record UserSerialization(
    string? role,
    int id,
    string? firstName,
    string? lastName,
    string? email,
    string? password,
    int? bookingId,
    int? flightId,
    string? flightClass
    );