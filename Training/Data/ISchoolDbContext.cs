using System;
using System.Data.Entity;

namespace Training.Data
{
    public interface ISchoolDbContext : IDisposable
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        int SaveChanges();
    }
}