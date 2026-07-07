using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    /// <summary>
    /// Provides persistence operations for <see cref="Rol" /> entities.
    /// </summary>
    public interface IRolRepository : IRepository<Rol, long>
    {
        Task<Rol?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Rol>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken = default);
    }
}
