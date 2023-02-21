using System.Linq;
using Training.Models;

namespace Training.Services
{
    public interface ISchoolService
    {
        IQueryable<School> GetSchools();
        School GetSchool(int id);
        void InsertSchool(School school);
        void UpdateSchool(School school);
        void DeleteSchool(School school);
    }
}