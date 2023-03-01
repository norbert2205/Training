using Training.Data;

namespace Training.Models
{
    public class Assignment : BaseEntity, IAssignment
    {
        public string Name { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public string CorrectAnswer { get; set; }

        public int Grade { get; set; }

        public Course Course { get; set; }

        public User User { get; set; }
    }
}