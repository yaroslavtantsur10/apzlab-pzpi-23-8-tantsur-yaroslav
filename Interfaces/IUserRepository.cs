using ComfortSpace.Models;

namespace ComfortSpace.Interfaces
{
    public interface IUserRepository
    {

        bool CreateUser(User user);
        User GetUser(int id);
        User GetByEmail(string email);
        ICollection<User> GetUsers();

        bool UserExists(int id);
        bool EmailExists(string email);

        bool UpdateUser(User user);

        bool DeleteUser(User user);

        bool Save();
    }
}