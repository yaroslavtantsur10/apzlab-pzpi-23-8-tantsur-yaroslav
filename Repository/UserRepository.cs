using ComfortSpace.Data;
using ComfortSpace.Interfaces;
using ComfortSpace.Models;

namespace ComfortSpace.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users
                .OrderBy(u => u.UserId)
                .ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users
                .FirstOrDefault(u => u.UserId == id);
        }

        public bool UserExists(int id)
        {
            return _context.Users
                .Any(u => u.UserId == id);
        }

        public bool UpdateUser(User user)
        {
            _context.Users.Update(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Users.Remove(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public User GetByEmail(string email)
        {
            return _context.Users
                .FirstOrDefault(u => u.Email == email);
        }

        public bool EmailExists(string email)
        {
            return _context.Users
                .Any(u => u.Email == email);
        }

        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            return Save();
        }
    }
}
