using System.Data.Entity.ModelConfiguration;
using Training.Models;

namespace Training.Configurations
{
    public class CourseConfiguration : EntityTypeConfiguration<Course>
    {
        public CourseConfiguration()
        {
            HasKey(_ => _.Id);

            Property(_ => _.Name);

            ToTable("Courses");
        }
    }
}