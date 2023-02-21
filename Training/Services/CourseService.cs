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
            return await _courseRepository.GetById(id)
                .FirstOrDefaultAsync();
        }

        public void CreateCourse(Course course)
        {
            _courseRepository.Create(course);
        }

        public void UpdateCourse(Course course)
        {
            _courseRepository.Update(course);
        }

        public void DeleteCourse(Course course)
        {
            _courseRepository.Delete(course);
        }
    }
}