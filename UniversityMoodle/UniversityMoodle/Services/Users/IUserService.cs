using UniversityMoodle.Models;

namespace UniversityMoodle.Services.Users
{
    public interface IUserService
    {
        bool UserExist(string email);
        bool CreateUser(User user);
        bool Save();
    }
}
