using AirportTicketBooking.Airport;
using AirportTicketBooking.Class;
using AirportTicketBooking.Country;
using AirportTicketBooking.Flight.FlightModel;
using AirportTicketBooking.User.Manager.ManagerModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBooking.User.UserRepository
{
    public class UserRepository
    {
        public List<UserModel> Users { get; set; } = new List<UserModel>();

        public UserRepository()
        {
            const string path = "C:\\Users\\M.T\\Desktop\\projects\\foothill\\practice-projects\\AirportTicketBooking\\AirportTicketBooking\\User\\UserRepository\\Users.csv";
            using (var reader = new StreamReader(path))
            {
                var headerLine = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    string role = values[0];
                    //Console.WriteLine($"role: {role}");
                    UserModel user = new UserModel();
                    if (role.Equals("passenger", StringComparison.OrdinalIgnoreCase))
                    {
                        user = new Passenger.PassengerModel.PassengerModel()
                        {
                            Id = int.Parse(values[1]),
                            FirstName = values[2],
                            LastName = values[3],
                            Email = values[4],
                            Password = values[5]
                        };
                    }
                    else if (role.Equals("manager", StringComparison.OrdinalIgnoreCase))
                    {
                        user = new ManagerModel()
                        {
                            Id = int.Parse(values[1]),
                            FirstName = values[2],
                            LastName = values[3],
                            Email = values[4],
                            Password = values[5]
                        };
                    }

                    Users.Add(user);
                }
            }
        }

        public UserModel Login(string email, string password)
        {
            return Users.FirstOrDefault(user => user.Email.Equals(email) && user.Password.Equals(password));
        }
    }
}
