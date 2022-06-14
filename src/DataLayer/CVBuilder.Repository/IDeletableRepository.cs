using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CVBuilder.EFContext.Transaction;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Repository
{
    public interface IDeletableRepository<TEntity, TKey>
        where TEntity : IDeletableEntity<TKey>
    {
        IQueryable<TEntity> TableWithDeleted { get; }
        IQueryable<TEntity> Table { get; }

        Task<TEntity> GetByIdAsync(TKey id, string includeProperties = null);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey id);
        Task DeleteAsync(TEntity entity);
        Task<TEntity> RecoverAsync(TKey id);
        
        ITransactionWrapper BeginTransaction();
        Task SaveChangesAsync();
    }
}