using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _courseRepository.GetAll
                .ToListAsync();
        }

        public async Task<Course> GetCourseAsync(int id)
        {
            return await _courseRepository.FindAsync(_ => _.Id == id);
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            return await _courseRepository.CreateAsync(course);
        }

        public async Task<Course> UpdateCourseAsync(Course course)
        {
            return await _courseRepository.UpdateAsync(course);
        }

        public async Task<int> DeleteCourseAsync(Course course)
        {
            return await _courseRepository.DeleteAsync(course);
        }
    }
}