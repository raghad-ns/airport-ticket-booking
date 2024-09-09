using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.FileProcessor.Deserializer.Flight;

namespace TicketBooking.FileProcessor;

public interface IDeserialize
{
    public static abstract FlightSerialization Deserialize(string[] values);
}
