using System.Data.Entity.ModelConfiguration;
using Training.Models;

namespace Training.Mapping
{
    public class CourseMap : EntityTypeConfiguration<Course>
    {
        public CourseMap()
        {
            HasKey(_ => _.Id);

            Property(_ => _.Name);

            ToTable("Courses");
        }
    }
}