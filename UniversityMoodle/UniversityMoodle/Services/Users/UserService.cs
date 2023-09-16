using UniversityMoodle.Data;
using UniversityMoodle.Models;

namespace UniversityMoodle.Services.Users
{
    public class UserService : IUserService
    {
        private DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool UserExist(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(x => x.Id).ToList();
        }

        public Role GetRole(int id)
        {
            return (Role)_context.Users.Where(u => u.Id == id).Select(u => u.Role).FirstOrDefault();
        }
    }
}
