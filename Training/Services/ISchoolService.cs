using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Training.Models;

namespace Training.Services
{
    public interface ISchoolService
    {
        Task<IEnumerable<School>> GetSchoolsAsync(CancellationToken token);

        Task<School> GetSchoolAsync(int id, CancellationToken token);

        Task<School> CreateSchoolAsync(School school, CancellationToken token);

        Task<int> DeleteSchoolAsync(School school, CancellationToken token);

        Task<School> UpdateSchoolAsync(School school, CancellationToken token);
    }
}