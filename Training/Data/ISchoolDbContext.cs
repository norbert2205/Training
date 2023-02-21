using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Training.Data
{
    public interface ISchoolDbContext : IDisposable
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        Task SaveChangesAsync();
    }
}