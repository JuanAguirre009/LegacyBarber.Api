using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    /// <summary>
    /// Provides persistence operations for <see cref="CategoriaServicio"/> aggregate roots.
    /// </summary>
    public interface ICategoriaServicioRepository : IRepository<CategoriaServicio, long>
    {
        Task<IReadOnlyList<CategoriaServicio>> GetAllByBarberiaAsync(long barberiaId, CancellationToken cancellationToken = default);
    }
}
