using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task<IEnumerable<Assignment>> GetAssignmentsAsync()
        {
            return await _repository.GetAll
                .ToListAsync();
        }

        public async Task<Assignment> GetAssignmentAsync(int id)
        {
            return await _repository.FindAsync(_ => _.Id == id);
        }

        public async Task<Assignment> CreateAssignmentAsync(Assignment assignment)
        {
            return await _repository.CreateAsync(assignment);
        }

        public async Task<Assignment> UpdateAssignmentAsync(Assignment assignment)
        {
            return await _repository.UpdateAsync(assignment);
        }

        public async Task<int> DeleteAssignmentAsync(Assignment assignment)
        {
            return await _repository.DeleteAsync(assignment);
        }
    }
}