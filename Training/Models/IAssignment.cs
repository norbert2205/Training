using Training.Data;

namespace Training.Models
{
    public interface IAssignment : IBaseEntity
    {
        string Name { get; set; }

        string Question { get; set; }

        string Answer { get; set; }

        string CorrectAnswer { get; set; }

        int Grade { get; set; }

        Course Course { get; set; }

        User User { get; set; }
    }
}