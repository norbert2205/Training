using System.Linq;
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

        public IQueryable<Assignment> GetAssignments()
        {
            return _assignmentRepository.Table;
        }

        public Assignment GetAssignment(int id)
        {
            return _assignmentRepository.GetById(id);
        }

        public void InsertAssignment(Assignment assignment)
        {
            _assignmentRepository.Insert(assignment);
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