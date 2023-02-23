using System.Data.Entity.ModelConfiguration;
using Training.Models;

namespace Training.Configurations
{
    public class RefreshTokenConfiguration : EntityTypeConfiguration<RefreshToken>
    {
        public RefreshTokenConfiguration()
        {
            HasKey(_ => _.Id);

            Property(_ => _.Token);
            Property(_ => _.TokenType);
            Property(_ => _.Expiration);
            Property(_ => _.User);

            ToTable("RefreshTokens");
        }
    }
}