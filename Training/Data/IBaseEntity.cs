using System;

namespace Training.Data
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
    }
}