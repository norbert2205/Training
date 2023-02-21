using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Models;

namespace Training.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetCoursesAsync();

        Task<Course> GetCourseAsync(int id);

        void CreateCourse(Course course);

        void UpdateCourse(Course course);

        void DeleteCourse(Course course);
    }
}