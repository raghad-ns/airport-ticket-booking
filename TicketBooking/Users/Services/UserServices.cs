using TicketBooking.Users.Repository;

namespace TicketBooking.Users.Services;

public class UserServices
{
    private readonly UserRepository _userRepository = UserRepository.GetInstance();
    public UserModel Login(string email, string password)
    {
        return _userRepository.Login(email, password);
    }

    public List<UserModel> GetUsers() => _userRepository.GetUsers();
}
