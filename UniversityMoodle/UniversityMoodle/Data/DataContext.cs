using Microsoft.EntityFrameworkCore;
using UniversityMoodle.Models;

namespace UniversityMoodle.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<TeacherStudent> TeacherStudents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeacherStudent>()
                .HasKey(ts => new { ts.TeacherId, ts.StudentId });
            modelBuilder.Entity<TeacherStudent>()
                .HasOne(t => t.Teacher)
                .WithMany(ts => ts.TeacherStudents)
                .HasForeignKey(t => t.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TeacherStudent>()
                .HasOne(s => s.Student)
                .WithMany(ts => ts.StudentTeachers)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });
            modelBuilder.Entity<UserCourse>()
                .HasOne(s => s.User)
                .WithMany(sc => sc.UserCourses)
                .HasForeignKey(s => s.StudentId);
            modelBuilder.Entity<UserCourse>()
                .HasOne(s => s.Course)
                .WithMany(sc => sc.UserCourses)
                .HasForeignKey(s => s.CourseId);
        }
    }
}
