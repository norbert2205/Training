using System.Linq;
using Training.Data;
using Training.Models;

namespace Training.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly IRepository<School> _schoolRepository;

        public SchoolService(IRepository<School> schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public IQueryable<School> GetSchools()
        {
            return _schoolRepository.Table;
        }

        public School GetSchool(int id)
        {
            return _schoolRepository.GetById(id);
        }

        public void InsertSchool(School school)
        {
            _schoolRepository.Insert(school);
        }

        public void UpdateSchool(School school)
        {
            _schoolRepository.Update(school);
        }

        public void DeleteSchool(School school)
        {
            _schoolRepository.Delete(school);
        }
    }
}