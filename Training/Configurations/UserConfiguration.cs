using System.Data.Entity.ModelConfiguration;
using Training.Models;

namespace Training.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
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

            ToTable("Users");
        }
    }
}