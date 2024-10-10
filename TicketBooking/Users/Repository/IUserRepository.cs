using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Users.Repository;

public interface IUserRepository
{
    UserModel Login(string email, string password);
    List<UserModel> GetUsers();
}