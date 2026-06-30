using System.Linq.Expressions;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    /// <summary>
    /// Generic repository for aggregate root entities.
    /// </summary>
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
