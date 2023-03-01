using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Training.Data;
using Training.Models;

namespace Training.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IRepository<Assignment> _repository;

        public AssignmentService(IRepository<Assignment> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsAsync(CancellationToken token)
        {
            return await _repository.GetAll
                .ToListAsync(token);
        }

        public async Task<Assignment> GetAssignmentAsync(int id, CancellationToken token)
        {
            return await _repository.FindAsync(_ => _.Id == id, token);
        }

        public async Task<Assignment> CreateAssignmentAsync(Assignment assignment, CancellationToken token)
        {
            return await _repository.CreateAsync(assignment, token);
        }

        public async Task<Assignment> UpdateAssignmentAsync(Assignment assignment, CancellationToken token)
        {
            return await _repository.UpdateAsync(assignment, token);
        }

        public async Task<int> DeleteAssignmentAsync(Assignment assignment, CancellationToken token)
        {
            return await _repository.DeleteAsync(assignment, token);
        }
    }
}