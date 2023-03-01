using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Training.Data
{
    public interface ISchoolDbContext : IDisposable
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        Task<int> SaveChangesAsync(CancellationToken token);

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : BaseEntity;
    }
}