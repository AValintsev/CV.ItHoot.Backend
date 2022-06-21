using System;
using System.Collections.Generic;
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
        Task RemoveManyAsync(IEnumerable<TEntity> entities);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null);
        Task<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter, string includeProperties = null);
        Task<(int count, List<TEntity> data)> GetListExtendedAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, TEntity>> select = null,
            string sort = null,
            int? skip = default,
            int? take = default,
            string includeProperties = null,
            bool asNoTracking = true,
            bool calculateCount = false);
        Task<(int count, List<TDto> data)> GetListDtoExtendedAsync<TDto>(
            Expression<Func<TEntity, TDto>> select,
            Expression<Func<TEntity, bool>> filter = null,
            string sort = null,
            int? skip = default,
            int? take = default,
            bool calculateCount = false);
    }
}