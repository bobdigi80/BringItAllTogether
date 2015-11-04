using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using BringingItAllTogether.Core;
using BringingItAllTogether.Interfaces;

namespace BringingItAllTogether.Data
{
   public class Repository<T> : IRepository<T> where T: BaseEntity
    {
        private readonly IDbContext _context;
        private IDbSet<T> _entities;

        public Repository(IDbContext context)
        {
            _context = context;
        }

       public virtual IEnumerable<T> Get()
       {
           IQueryable<T> query = _entities;
           return query.ToList();
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
                    throw new ArgumentNullException("entity");
                }
                entity.AddedDate = DateTime.UtcNow;
                entity.ModifiedDate = DateTime.UtcNow;
                Entities.Add(entity);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
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
                    throw new ArgumentNullException("entity");
                }
                entity.ModifiedDate = DateTime.UtcNow;
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
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
                    throw new ArgumentNullException("entity");
                }
                Entities.Remove(entity);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);                
                throw fail;
            }
        }

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(object id)
        {
            var entityToDelete = _entities.Find(id);
            Delete(entityToDelete);
        }
       
       public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }

       public IEnumerable<T> GetMany(Func<T, bool> @where)
       {
           return _entities.Where(where).ToList();
       }

       public IQueryable<T> GetManyQueryable(Func<T, bool> @where)
       {
           return _entities.Where(where).AsQueryable();
       }

       public T Get(Func<T, bool> @where)
       {
           return _entities.Where(where).FirstOrDefault();
       }

       public void Delete(Func<T, bool> @where)
       {
           IQueryable<T> objects = _entities.Where(where).AsQueryable();
           foreach (T obj in objects)
               _entities.Remove(obj);
       }

       public IEnumerable<T> GetAll()
       {
           return _entities.ToList();
       }

       public IQueryable<T> GetWithInclude(Expression<Func<T, bool>> predicate, params string[] include)
       {
           IQueryable<T> query = _entities;
           query = include.Aggregate(query, (current, inc) => current.Include(inc));
           return query.Where(predicate);
       }

       public bool Exists(object primaryKey)
       {
           return _entities.Find(primaryKey) != null;
       }

       public T GetSingle(Func<T, bool> predicate)
       {
           return _entities.Single(predicate);
       }

       public T GetFirst(Func<T, bool> predicate)
       {
           return _entities.First(predicate);
       }

       private IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<T>();
                }
                return _entities;
            }
        }
    }
}
