using UniversityMoodle.Models;

namespace UniversityMoodle.Services.Courses
{
    public class CourseService : ICourseService
    {
        public Course GetCourse(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Course> GetCourses()
        {
            throw new NotImplementedException();
        }

        public ICollection<Course> GetCoursesDoneByStudent(int studentId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Course> GetCoursesDoneByTeacher(int teacherId)
        {
            throw new NotImplementedException();
        }
    }
}
