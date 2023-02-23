using System.Data.Entity.ModelConfiguration;
using Training.Models;

namespace Training.Configurations
{
    public class LoginResponseConfiguration : EntityTypeConfiguration<LoginResponse>
    {
        public LoginResponseConfiguration()
        {
            HasKey(_ => _.Id);

            Property(_ => _.UserId);
            Property(_ => _.Token);
            Property(_ => _.AccessTokenExpiry);

            ToTable("LoginResponses");
        }
    }
}