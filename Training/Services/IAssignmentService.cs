using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Training.Models;

namespace Training.Services
{
    public interface IAssignmentService
    {
        Task<IEnumerable<Assignment>> GetAssignmentsAsync(CancellationToken token);

        Task<Assignment> GetAssignmentAsync(int id, CancellationToken token);

        Task<Assignment> CreateAssignmentAsync(Assignment assignment, CancellationToken token);

        Task<Assignment> UpdateAssignmentAsync(Assignment assignment, CancellationToken token);

        Task<int> DeleteAssignmentAsync(Assignment assignment, CancellationToken token);
    }
}