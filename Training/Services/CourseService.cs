using System.Linq;
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

        public IQueryable<Course> GetCourses()
        {
            return _courseRepository.Table;
        }

        public Course GetCourse(int id)
        {
            return _courseRepository.GetById(id);
        }

        public void InsertCourse(Course course)
        {
            _courseRepository.Insert(course);
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