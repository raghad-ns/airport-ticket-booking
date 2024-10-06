using TicketBooking.Users.Repository;

namespace TicketBooking.Users.Services;

public class UserServices
{
    private UserRepository _userRepository;
    public UserServices(List<UserModel> users) {
        _userRepository = UserRepository.GetInstance(users);
    }
    public UserModel Login(string email, string password)
    {
        return _userRepository.Login(email, password);
    }

    public List<UserModel> GetUsers() => _userRepository.GetUsers();
}