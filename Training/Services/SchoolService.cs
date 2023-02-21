using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<School>> GetSchoolsAsync()
        {
            return await _schoolRepository.GetAll
                .ToListAsync();
        }

        public async Task<School> GetSchoolAsync(int id)
        {
            return await _schoolRepository.GetById(id)
                .FirstOrDefaultAsync();
        }

        public void InsertSchool(School school)
        {
            _schoolRepository.Create(school);
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