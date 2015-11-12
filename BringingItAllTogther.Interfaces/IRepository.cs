using System;
using System.Collections.Generic;
using System.Linq;
using BringingItAllTogether.Core;

namespace BringingItAllTogether.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> Get();
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
        void Delete(T entity);
        IQueryable<T> Table { get; }
        IEnumerable<T> GetMany(Func<T, bool> where);
        IQueryable<T> GetManyQueryable(Func<T, bool> where);
        T Get(Func<T, Boolean> where);
        void Delete(Func<T, Boolean> where);
        IEnumerable<T> GetAll();
        IQueryable<T> GetWithInclude(
            System.Linq.Expressions.Expression<Func<T,
                bool>> predicate, params string[] include);
        bool Exists(object primaryKey);
        T GetSingle(Func<T, bool> predicate);
        T GetFirst(Func<T, bool> predicate);

    }
}
