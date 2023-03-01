using MySql.Data.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Training.Models;

namespace Training.Data
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class SchoolDbContext : DbContext, ISchoolDbContext
    {
        public SchoolDbContext() : base(nameof(School))
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public new DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            return base.Entry(entity);
        }

        public new async Task<int> SaveChangesAsync(CancellationToken token)
        {
            TrackChanges();
            return await base.SaveChangesAsync(token);
        }

        private void TrackChanges()
        {
            var entityEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            foreach (var entityEntry in entityEntries)
            {
                if (entityEntry.Property("CreatedAt") != null)
                {
                    if (entityEntry.State == EntityState.Added)
                    {
                        entityEntry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                    }
                }

                if (entityEntry.Property("ModifiedAt") != null)
                {
                    entityEntry.Property("ModifiedAt").CurrentValue = DateTime.UtcNow;
                }
            }
        }
    }
}