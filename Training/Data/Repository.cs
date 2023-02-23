using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Training.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ISchoolDbContext _ctx;
        private IDbSet<T> _entities;

        public Repository(ISchoolDbContext ctx)
        {
            _ctx = ctx;
        }

        protected IDbSet<T> Entities => _entities ?? (_entities = _ctx.Set<T>());

        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                Entities.Add(entity);
                await _ctx.SaveChangesAsync();

                return entity;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}" + Environment.NewLine;
                    }
                }

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                var exist = GetById(entity.Id);

                if (exist != null)
                {
                    _ctx.Entry(exist).CurrentValues.SetValues(entity);
                    await _ctx.SaveChangesAsync();
                }
                return exist;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public async Task<int> DeleteAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                Entities.Remove(entity);
                return await _ctx.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public T GetById(int id)
        {
            return Entities.Find(id);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _ctx.Set<T>().SingleOrDefaultAsync(match);
        }

        public IQueryable<T> GetAll => Entities;
    }
}