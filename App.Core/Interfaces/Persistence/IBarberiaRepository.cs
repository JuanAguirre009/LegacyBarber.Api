using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    /// <summary>
    /// Provides persistence operations for <see cref="Barberia"/> aggregate roots.
    /// </summary>
    public interface IBarberiaRepository : IRepository<Barberia, long>
    {
        Task<bool> ExisteSlugAsync(string slug, CancellationToken cancellationToken = default);
        Task<bool> ExisteEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
