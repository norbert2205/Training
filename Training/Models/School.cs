using Training.Data;

namespace Training.Models
{
    public class School : BaseEntity, ISchool
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Logo { get; set; }
    }
}