namespace TicketBooking.Users.Repository;

public class UserRepository
{
    public static List<UserModel> Users { get; set; } = new List<UserModel>();
    private static UserRepository _instance ;

    private UserRepository(List<UserModel> users)
    {
        Users = users;
    }

    public static UserRepository GetInstance(List<UserModel> users)
    {
        _instance ??= new(users);
        return _instance;
    }

    public List<UserModel> GetUsers() => Users;


    public UserModel Login(string email, string password)
    {
        return Users.FirstOrDefault(user => user.Email.Equals(email) && user.Password.Equals(password));
    }
}