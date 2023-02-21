using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Models;

namespace Training.Services
{
    public interface IAssignmentService
    {
        Task<IEnumerable<Assignment>> GetAssignmentsAsync();

        Task<Assignment> GetAssignmentAsync(int id);

        void CreateAssignment(Assignment assignment);

        void UpdateAssignment(Assignment assignment);

        void DeleteAssignment(Assignment assignment);
    }
}