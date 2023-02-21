using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Training.Data;
using Training.Models;

namespace Training.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IRepository<Assignment> _assignmentRepository;

        public AssignmentService(IRepository<Assignment> assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsAsync()
        {
            return await _assignmentRepository.GetAll
                .ToListAsync();
        }

        public async Task<Assignment> GetAssignmentAsync(int id)
        {
            return await _assignmentRepository.GetById(id)
                .FirstOrDefaultAsync();
        }

        public void CreateAssignment(Assignment assignment)
        {
            _assignmentRepository.Create(assignment);
        }

        public void UpdateAssignment(Assignment assignment)
        {
            _assignmentRepository.Update(assignment);
        }

        public void DeleteAssignment(Assignment assignment)
        {
            _assignmentRepository.Delete(assignment);
        }
    }
}