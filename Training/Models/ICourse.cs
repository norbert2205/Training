using System.Collections.Generic;
using Training.Data;

namespace Training.Models
{
    public interface ICourse : IBaseEntity
    {
        string Name { get; set; }

        ICollection<User> Users { get; set; }
    }
}