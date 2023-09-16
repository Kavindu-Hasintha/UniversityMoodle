using System.Security.Claims;
using UniversityMoodle.Data;
using UniversityMoodle.Models;

namespace UniversityMoodle.Services.Users
{
    public class UserService : IUserService
    {
        private DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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

        public string GetEmail()
        {
            string email = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            }
            return email;
        }

        public User GetUser(string email)
        {
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }
    }
}
