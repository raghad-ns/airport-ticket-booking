using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.FileProcessor.Deserializer.User
{
    public class UserDeserializer : IDeserialize<UserSerialization>
    {
        public static UserSerialization Deserialize(string[] values)
        {
            return new UserSerialization(
                role: values[0], 
                id: string.IsNullOrEmpty(values[1]) ? 0 : int.Parse(values[1]),
                firstName: values[2],
                lastName: values[3],
                email: values[4],
                password: values[5],
                bookingId: values.Length > 6 ? int.Parse(values[6]) : null,
                flightId: values.Length > 6 ? int.Parse(values[7]) : null,
                flightClass: values.Length > 6 ? values[8] : null
            );
        }

    }
}
