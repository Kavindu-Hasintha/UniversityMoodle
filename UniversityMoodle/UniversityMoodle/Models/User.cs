namespace UniversityMoodle.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DoB { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }
        public Role Role { get; set; }
        public ICollection<TeacherStudent> TeacherStudents { get; set; }
        public ICollection<TeacherStudent> StudentTeachers { get; set; }
    }
}
