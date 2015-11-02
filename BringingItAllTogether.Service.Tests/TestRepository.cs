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
        
        public virtual void Delete(T entity)
        {
            repository.Remove(entity);
        }

        public virtual void Update(T entity)
        {
            repository.Remove(entity);
            repository.Add(entity);
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
