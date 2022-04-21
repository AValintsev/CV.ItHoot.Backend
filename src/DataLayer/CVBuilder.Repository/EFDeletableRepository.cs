using System;
using CVBuilder.EFContext;
using CVBuilder.Models.Entities.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CVBuilder.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

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
    }
}