using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Models;

namespace Training.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetCoursesAsync();

        Task<Course> GetCourseAsync(int id);

        Task<Course> CreateCourseAsync(Course course);

        Task<Course> UpdateCourseAsync(Course course);

        Task<int> DeleteCourseAsync(Course course);
    }
}