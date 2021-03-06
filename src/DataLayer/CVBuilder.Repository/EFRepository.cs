using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CVBuilder.Models.Entities.Interfaces;
using CVBuilder.EFContext;
using CVBuilder.EFContext.Transaction;
using CVBuilder.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Repository
{
    public class EFRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        protected readonly EFDbContext DbContext;
        protected readonly DbSet<TEntity> Entity;

        private DbContextTransactionWrapper CurrentTransaction { get; set; }

        public EFRepository(EFDbContext dataContext)
        {
            DbContext = dataContext;
            Entity = dataContext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Table => Entity.AsQueryable();

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            Entity.Add(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<TEntity>> CreateManyAsync(IEnumerable<TEntity> entities)
        {
            Entity.AddRange(entities);
            await DbContext.SaveChangesAsync();

            return entities;
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            Entity.Remove(await GetByIdAsync(id));

            await DbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            Entity.Remove(entity);

            await DbContext.SaveChangesAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id, string includeProperties = null)
        {
            return await Entity.IncludeProperties(includeProperties)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null)
        {
            var query = Entity.IncludeProperties(includeProperties);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var result = await query.Take(200).ToListAsync();
            return result;
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            var query = Entity.AsQueryable();
            if (filter != null)
            {
                return await query.AnyAsync(filter);
            }

            return await query.AnyAsync();
        }

        public virtual async Task<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter, string includeProperties = null)
        {
            return await Entity.IncludeProperties(includeProperties)
                .Where(filter)
                .FirstOrDefaultAsync();
        }

        public async Task<(int count, List<TDto> data)> GetListDtoExtendedAsync<TDto>(
            Expression<Func<TEntity, TDto>> select,
            Expression<Func<TEntity, bool>> filter = null,
            string sort = null,
            int? skip = null,
            int? take = 100,
            bool calculateCount = false)
        {
            var count = 0;
            var query = Entity.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (calculateCount)
            {
                count = query.Count();
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            if (!string.IsNullOrEmpty(sort))
            {
                query = query.AsQueryable();
                query = query.OrderBy(sort);
            }

            return (count, await query.Select(select).ToListAsync());
        }

        public ITransactionWrapper BeginTransaction()
        {
            if (CurrentTransaction != null && !CurrentTransaction.IsDisposed)
            {
                return new NestedTransactionWrapper(CurrentTransaction);
            }

            CurrentTransaction = new DbContextTransactionWrapper(DbContext);
            return CurrentTransaction;
        }

        public async Task<(int count, List<TEntity> data)> GetListExtendedAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, TEntity>> select = null,
            string sort = null,
            int? skip = null,
            int? take = 100,
            string includeProperties = null,
            bool asNoTracking = true,
            bool calculateCount = false)
        {
            var count = 0;
            var query = Entity.IncludeProperties(includeProperties);
            
            if (select != null)
            {
                query = query.Select(select);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (calculateCount)
            {
                count = query.Count();
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            if (!string.IsNullOrEmpty(sort))
            {
                query = query.AsQueryable();
                query = query.OrderBy(sort);
            }

            return (count, await query.ToListAsync());
        }

        public async Task RemoveManyAsync(IEnumerable<TEntity> entities)
        {
            Entity.RemoveRange(entities);

            await DbContext.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Entity.Update(entity);

            await DbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            Entity.UpdateRange(entities);

            await DbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }

 
    }
}