using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Training.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ISchoolDbContext _ctx;

        public Repository(ISchoolDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Create(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                _ctx.Set<T>().Add(entity);
                _ctx.SaveChangesAsync();
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

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                _ctx.SaveChangesAsync();
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

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                _ctx.Set<T>().Remove(entity);
                _ctx.SaveChangesAsync();
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

        public IQueryable<T> GetById(int id)
        {
            return _ctx.Set<T>().Where(_ => _.Id == id).AsNoTracking();
        }

        public IQueryable<T> GetAll => _ctx.Set<T>().AsNoTracking();
    }
}