using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    /// <summary>
    /// Provides persistence operations for <see cref="Usuario" /> aggregate roots.
    /// </summary>
    public interface IUsuarioRepository : IRepository<Usuario, long>
    {
        Task<Usuario?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
