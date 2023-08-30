namespace UniversityMoodle.Models
{
    public class UserCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public User User { get; set; }
        public Course Course { get; set; }
    }
}
