using UniversityMoodle.Data;
using UniversityMoodle.Models;

namespace UniversityMoodle.Services.Roles
{
    public class RoleService : IRoleService
    {
        private DataContext _context;

        public RoleService(DataContext context)
        {
            _context = context;
        }

        public Role GetRole(string name)
        {
            return _context.Roles.Where(r => r.Name == name).FirstOrDefault();
        }

        public Role GetRole(int id)
        {
            return _context.Roles.Where(r => r.Id == id).FirstOrDefault();
        }
    }
}
