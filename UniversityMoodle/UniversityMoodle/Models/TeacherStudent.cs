namespace UniversityMoodle.Models
{
    public class TeacherStudent
    {
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public User Teacher { get; set; }
        public User Student { get; set; }
    }
}
