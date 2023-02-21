using System.Collections.Generic;

namespace Training.Models
{
    public interface IAssignment
    {
        string Name { get; set; }

        string Question { get; set; }

        string Answer { get; set; }

        string CorrectAnswer { get; set; }

        int Grade { get; set; }
        ICollection<User> Users { get; set; }
    }
}