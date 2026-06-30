using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    /// <summary>
    /// Provides persistence operations for <see cref="Servicio"/> aggregate roots.
    /// </summary>
    public interface IServicioRepository : IRepository<Servicio, long>
    {
    }
}
