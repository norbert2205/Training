using System.Data.Entity.ModelConfiguration;
using Training.Models;

namespace Training.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(_ => _.Id);

            Property(_ => _.FirstName);
            Property(_ => _.LastName);
            Property(_ => _.PhoneNumber);
            Property(_ => _.Type);

            HasMany(u => u.Courses)
                .WithMany(c => c.Users)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserId");
                    cs.MapRightKey("CourseId");
                    cs.ToTable("UserCourse");
                });

            HasMany(u => u.Assignments)
                .WithMany(a => a.Users)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserId");
                    cs.MapRightKey("AssignmentId");
                    cs.ToTable("UserAssignment");
                });

            ToTable("Users");
        }
    }
}