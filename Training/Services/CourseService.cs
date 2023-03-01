using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Training.Data;
using Training.Models;

namespace Training.Services
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _courseRepository;

        public CourseService(IRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync(CancellationToken token)
        {
            return await _courseRepository.GetAll
                .ToListAsync(token);
        }

        public async Task<Course> GetCourseAsync(int id, CancellationToken token)
        {
            return await _courseRepository.FindAsync(_ => _.Id == id, token);
        }

        public async Task<Course> CreateCourseAsync(Course course, CancellationToken token)
        {
            return await _courseRepository.CreateAsync(course, token);
        }

        public async Task<Course> UpdateCourseAsync(Course course, CancellationToken token)
        {
            return await _courseRepository.UpdateAsync(course, token);
        }

        public async Task<int> DeleteCourseAsync(Course course, CancellationToken token)
        {
            return await _courseRepository.DeleteAsync(course, token);
        }
    }
}