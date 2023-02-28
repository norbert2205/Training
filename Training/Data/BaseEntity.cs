using System;

namespace Training.Data
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}