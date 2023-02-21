using System.Collections.Generic;

namespace Training.Models
{
    public interface IUser
    {
        Type Type { get; set; }
        ICollection<Course> Courses { get; set; }
        ICollection<Assignment> Assignments { get; set; }
    }
}