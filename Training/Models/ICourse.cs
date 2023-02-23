using System.Collections.Generic;

namespace Training.Models
{
    public interface ICourse
    {
        string Name { get; set; }

        ICollection<User> Users { get; set; }
    }
}