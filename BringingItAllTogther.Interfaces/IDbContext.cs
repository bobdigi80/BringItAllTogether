using System.Data.Entity;
using BringingItAllTogether.Core;

namespace BringingItAllTogether.Interfaces
{
   public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        int SaveChanges();
    }
}
