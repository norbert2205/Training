using System.Data.Entity.ModelConfiguration;
using Training.Models;

namespace Training.Mapping
{
    public class AssignmentMap : EntityTypeConfiguration<Assignment>
    {
        public AssignmentMap()
        {
            HasKey(_ => _.Id);

            Property(_ => _.Answer);
            Property(_ => _.CorrectAnswer);
            Property(_ => _.Grade);
            Property(_ => _.Name);
            Property(_ => _.Question);

            ToTable("Assignments");
        }
    }
}