using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Training.Models;

namespace Training.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetCoursesAsync(CancellationToken token);

        Task<Course> GetCourseAsync(int id, CancellationToken token);

        Task<Course> CreateCourseAsync(Course course, CancellationToken token);

        Task<Course> UpdateCourseAsync(Course course, CancellationToken token);

        Task<int> DeleteCourseAsync(Course course, CancellationToken token);
    }
}