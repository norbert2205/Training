using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Training.Data;

namespace Training.Models
{
    public class User : BaseEntity, IUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Type Type { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();

        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

        [NotMapped]
        public string Token { get; set; }
    }
}