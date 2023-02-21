using System.Data.Entity.ModelConfiguration;
using Training.Models;

namespace Training.Mapping
{
    public class SchoolMap : EntityTypeConfiguration<School>
    {
        public SchoolMap()
        {
            HasKey(_ => _.Id);

            Property(_ => _.Name);
            Property(_ => _.Description);
            Property(_ => _.Logo);

            ToTable("Schools");
        }
    }
}