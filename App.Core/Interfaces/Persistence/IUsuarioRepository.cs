using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    /// <summary>
    /// Provides persistence operations for <see cref="Usuario" /> entities.
    /// </summary>
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<Usuario?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<IEnumerable<Usuario>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Usuario> CreateAsync(Usuario user, CancellationToken cancellationToken = default);
        void Update(Usuario user);
        void Delete(Usuario user);
    }
}
