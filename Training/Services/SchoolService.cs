using System.Collections.Generic;
using System.Data.Entity;
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
            return await _schoolRepository.FindAsync(_ => _.Id == id);
        }

        public async Task<School> CreateSchoolAsync(School school)
        {
            return await _schoolRepository.CreateAsync(school);
        }

        public async Task<School> UpdateSchoolAsync(School school)
        {
            return await _schoolRepository.UpdateAsync(school);
        }

        public async Task<int> DeleteSchoolAsync(School school)
        {
            return await _schoolRepository.DeleteAsync(school);
        }
    }
}