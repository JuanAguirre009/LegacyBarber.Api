using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LegacyBarber.App.DataAccess.Repos
{
    /// <summary>
    /// Base implementation of a generic repository over EF Core.
    /// </summary>
    internal class BaseRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class
    {
        protected readonly AppDbContext context;
        protected readonly DbSet<TEntity> dbSet;

        public BaseRepository(AppDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await dbSet.FindAsync(new object?[] { id }, cancellationToken);
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await dbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetFilteredAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await dbSet.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
        }

        public virtual async Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await dbSet.FindAsync(new object?[] { id }, cancellationToken) is not null;
        }

        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }
    }
}
