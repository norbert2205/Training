using System.Collections.Generic;
using Training.Data;

namespace Training.Models
{
    public class User : BaseEntity, IUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public Type Type { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();

        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
    }
}