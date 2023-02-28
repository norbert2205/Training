using Training.Data;

namespace Training.Models
{
    public interface ISchool : IBaseEntity
    {
        string Name { get; set; }

        string Description { get; set; }

        byte[] Logo { get; set; }
    }
}