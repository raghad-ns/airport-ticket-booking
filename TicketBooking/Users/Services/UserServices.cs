using TicketBooking.Users.Repository;

namespace TicketBooking.Users.Services;

public class UserServices
{
    private IUserRepository _userRepository;
    public UserServices(IUserRepository userRepository) {
        _userRepository = userRepository;
    }
    public UserModel Login(string email, string password)
    {
        return _userRepository.Login(email, password);
    }

    public List<UserModel> GetUsers() => _userRepository.GetUsers();
}