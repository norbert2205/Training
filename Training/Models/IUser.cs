using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Training.Data;

namespace Training.Models
{
    public interface IUser : IBaseEntity
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string PhoneNumber { get; set; }

        string Username { get; set; }

        string Password { get; set; }

        string Email { get; set; }

        Type Type { get; set; }

        ICollection<Course> Courses { get; set; }

        ICollection<Assignment> Assignments { get; set; }

        [NotMapped]
        string Token { get; set; }
    }
}