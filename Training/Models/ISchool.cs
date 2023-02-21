namespace Training.Models
{
    public interface ISchool
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        byte[] Logo { get; set; }
    }
}