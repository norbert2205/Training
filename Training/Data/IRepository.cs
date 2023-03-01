using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Training.Data
{
    public interface IRepository<T> where T : IBaseEntity
    {
        Task<T> CreateAsync(T entity, CancellationToken token);

        Task<T> UpdateAsync(T entity, CancellationToken to);

        Task<int> DeleteAsync(T entity, CancellationToken token);

        T GetById(int id);

        IQueryable<T> GetAll { get; }

        Task<T> FindAsync(Expression<Func<T, bool>> match, CancellationToken token);
    }
}