using System.Linq;
using Training.Models;

namespace Training.Services
{
    public interface ICourseService
    {
        IQueryable<Course> GetCourses();
        Course GetCourse(int id);
        void InsertCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(Course course);
    }
}