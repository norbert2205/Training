using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Training.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<int> DeleteAsync(T entity);

        T GetById(int id);

        IQueryable<T> GetAll { get; }

        Task<T> FindAsync(Expression<Func<T, bool>> match);
    }
}