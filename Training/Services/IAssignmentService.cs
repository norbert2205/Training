using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Models;

namespace Training.Services
{
    public interface IAssignmentService
    {
        Task<IEnumerable<Assignment>> GetAssignmentsAsync();

        Task<Assignment> GetAssignmentAsync(int id);

        Task<Assignment> CreateAssignmentAsync(Assignment assignment);

        Task<Assignment> UpdateAssignmentAsync(Assignment assignment);

        Task<int> DeleteAssignmentAsync(Assignment assignment);
    }
}