using System.Linq;
using Training.Models;

namespace Training.Services
{
    public interface IAssignmentService
    {
        IQueryable<Assignment> GetAssignments();
        Assignment GetAssignment(int id);
        void InsertAssignment(Assignment assignment);
        void UpdateAssignment(Assignment assignment);
        void DeleteAssignment(Assignment assignment);
    }
}