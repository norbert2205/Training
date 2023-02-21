using System.Collections.Generic;
using Training.Data;

namespace Training.Models
{
    public class Course : BaseEntity, ICourse
    {
        public string Name { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}