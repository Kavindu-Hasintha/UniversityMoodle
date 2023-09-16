using UniversityMoodle.Models;

namespace UniversityMoodle.Services.Roles
{
    public interface IRoleService
    {
        Role GetRole(string name);
        Role GetRole(int id);
    }
}
