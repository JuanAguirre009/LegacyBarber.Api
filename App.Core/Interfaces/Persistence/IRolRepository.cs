using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    /// <summary>
    /// Provides persistence operations for <see cref="Rol" /> entities.
    /// </summary>
    public interface IRolRepository
    {
        Task<Rol?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<Rol>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken = default);
    }
}
