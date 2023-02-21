using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

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

        public T GetById(object id)
        {
            return Entities.Find(id);
        }

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                Entities.Add(entity);
                _ctx.SaveChanges();
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
                _ctx.SaveChanges();
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
                Entities.Remove(entity);
                _ctx.SaveChanges();
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

        public virtual IQueryable<T> Table => Entities;

        private IDbSet<T> Entities => _entities ?? (_entities = _ctx.Set<T>());
    }
}