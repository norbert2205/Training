using System.Data.Entity.ModelConfiguration;
using Training.Models;

namespace Training.Configurations
{
    public class SchoolConfiguration : EntityTypeConfiguration<School>
    {
        public SchoolConfiguration()
        {
            HasKey(_ => _.Id);

            Property(_ => _.Name);
            Property(_ => _.Description);
            Property(_ => _.Logo);

            ToTable("Schools");
        }
    }
}