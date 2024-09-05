using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.User.UserServices;

public class UserServices
{
    private readonly UserRepository.UserRepository _userRepository = new();
    public UserModel Login(string email, string password) {
        return  _userRepository.Login(email, password);
    }
}
