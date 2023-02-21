using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Models;

namespace Training.Services
{
    public interface ISchoolService
    {
        Task<IEnumerable<School>> GetSchoolsAsync();

        Task<School> GetSchoolAsync(int id);

        void InsertSchool(School school);

        void UpdateSchool(School school);

        void DeleteSchool(School school);
    }
}