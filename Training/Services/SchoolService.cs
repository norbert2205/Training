using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
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

        public async Task<IEnumerable<School>> GetSchoolsAsync(CancellationToken token)
        {
            return await _schoolRepository.GetAll
                .ToListAsync(token);
        }

        public async Task<School> GetSchoolAsync(int id, CancellationToken token)
        {
            return await _schoolRepository.FindAsync(_ => _.Id == id, token);
        }

        public async Task<School> CreateSchoolAsync(School school, CancellationToken token)
        {
            return await _schoolRepository.CreateAsync(school, token);
        }

        public async Task<School> UpdateSchoolAsync(School school, CancellationToken token)
        {
            return await _schoolRepository.UpdateAsync(school, token);
        }

        public async Task<int> DeleteSchoolAsync(School school, CancellationToken token)
        {
            return await _schoolRepository.DeleteAsync(school, token);
        }
    }
}