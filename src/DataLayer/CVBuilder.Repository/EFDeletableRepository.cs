using CVBuilder.EFContext;
using CVBuilder.Models.Entities.Interfaces;
using CVBuilder.Repository.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CVBuilder.Repository
{
    public class EFDeletableRepository<TEntity, TKey> : EFRepository<TEntity, TKey>, IDeletableRepository<TEntity, TKey>
        where TEntity : class, IDeletableEntity<TKey>
    {
        private readonly IQueryable<TEntity> _entityNotDeleted;

        public EFDeletableRepository(EFDbContext dataContext)
            : base(dataContext)
        {
            _entityNotDeleted = Entity.Where(r => !r.DeletedAt.HasValue);
        }

        public virtual IQueryable<TEntity> TableWithDeleted => base.Table;

        public override IQueryable<TEntity> Table => _entityNotDeleted;

        public override async Task<TEntity> GetByIdAsync(TKey id, string includeProperties = null)
        {
            return await _entityNotDeleted.IncludeProperties(includeProperties)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public override async Task DeleteAsync(TKey id)
        {
            var entry = await GetByIdAsync(id);
            entry.DeletedAt = DateTime.UtcNow;

            await DbContext.SaveChangesAsync();
        }

        public override async Task DeleteAsync(TEntity entity)
        {
            entity.DeletedAt = DateTime.UtcNow;

            await DbContext.SaveChangesAsync();
        }

        public override async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
            {
                return await _entityNotDeleted.AnyAsync(filter);
            }

            return await _entityNotDeleted.AnyAsync();
        }

        public async Task<TEntity> RecoverAsync(TKey id)
        {
            var entity = await TableWithDeleted.FirstOrDefaultAsync(x => x.Id.Equals(id));
            entity.DeletedAt = null;

            await DbContext.SaveChangesAsync();

            return entity;
        }

        public override async Task RemoveManyAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                await DeleteAsync(entity);
            }
        }

        public override async Task<(int count, List<TDto> data)> GetListDtoExtendedAsync<TDto>(
            Expression<Func<TEntity, TDto>> select,
            Expression<Func<TEntity, bool>> filter = null,
            string sort = null,
            int? skip = null,
            int? take = 100,
            bool calculateCount = false)
        {
            var count = 0;
            var query = Table;

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

        public override async Task<(int count, List<TEntity> data)> GetListExtendedAsync(
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
            var query = Table.IncludeProperties(includeProperties);

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

        public override async Task<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter, string includeProperties = null)
        {
            return await Table.IncludeProperties(includeProperties)
                .Where(filter)
                .FirstOrDefaultAsync();
        }

        public override async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null)
        {
            var query = Table.IncludeProperties(includeProperties);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var result = await query.Take(200).ToListAsync();
            return result;
        }
    }
}