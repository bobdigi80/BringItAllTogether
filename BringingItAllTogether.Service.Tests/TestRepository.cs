using System.Data.Entity;
using BringingItAllTogether.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BringingItAllTogether.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BringingItAllTogether.Service.Tests
{
    [TestClass]  
    public class TestRepository<T> : IRepository<T> where T : BaseEntity
    {
        private List<T> repository;
        private readonly IDbContext _context;
        private IDbSet<T> _entities;

        public TestRepository(IDbContext context)
        {
            repository = new List<T>();
            _context = context;
        }

        #region IRepository<T> Members

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(T entity)
        {
            repository.Remove(entity);
        }

        public virtual void Update(T entity)
        {
            repository.Remove(entity);
            repository.Add(entity);
        }

        public IEnumerable<T> Get()
        {
            throw new NotImplementedException();
        }

        public T GetById(object id)
        {
            return repository.Find((Predicate<T>) id);
        }
        
        public void Insert(T entity)
        {
            repository.Add(entity);
        }

        public IQueryable<T> Table
        {
            get { return Entities; }
        }

        public IEnumerable<T> GetMany(Func<T, bool> @where)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetManyQueryable(Func<T, bool> @where)
        {
            throw new NotImplementedException();
        }

        public T Get(Func<T, bool> @where)
        {
            throw new NotImplementedException();
        }

        public void Delete(Func<T, bool> @where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetWithInclude(Expression<Func<T, bool>> predicate, params string[] include)
        {
            throw new NotImplementedException();
        }

        public bool Exists(object primaryKey)
        {
            throw new NotImplementedException();
        }

        public T GetSingle(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public T GetFirst(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
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
        #endregion
    }
}
