using UniversityMoodle.Models;

namespace UniversityMoodle.Services.Courses
{
    public interface ICourseRepository
    {
        ICollection<Course> GetCourses();
        Course GetCourse(int id);
        ICollection<Course> GetCoursesDoneByTeacher(int teacherId);
        ICollection<Course> GetCoursesDoneByStudent(int studentId);
    }
}
