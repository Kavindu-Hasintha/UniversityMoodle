using UniversityMoodle.Models;

namespace UniversityMoodle.Services.Users
{
    public interface IUserService
    {
        ICollection<User> GetUsers();
        bool UserExist(string email);
        bool CreateUser(User user);
        Role GetRole(int id);
        string GetEmail();
        User GetUser(string email);
        bool Save();
    }
}
