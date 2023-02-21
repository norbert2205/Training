using System.Linq;

namespace Training.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        IQueryable<T> GetById(int id);

        IQueryable<T> GetAll { get; }
    }
}