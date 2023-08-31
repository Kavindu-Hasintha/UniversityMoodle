using UniversityMoodle.Data;

namespace UniversityMoodle.Services.User
{
    public class UserService : IUserService
    {
        private DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        public bool UserExist(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
    }
}
