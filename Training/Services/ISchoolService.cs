using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Models;

namespace Training.Services
{
    public interface ISchoolService
    {
        Task<IEnumerable<School>> GetSchoolsAsync();

        Task<School> GetSchoolAsync(int id);

        Task<School> CreateSchoolAsync(School school);

        Task<int> DeleteSchoolAsync(School school);

        Task<School> UpdateSchoolAsync(School school);
    }
}