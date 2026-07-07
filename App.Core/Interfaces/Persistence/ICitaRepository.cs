using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    /// <summary>
    /// Provides persistence operations for <see cref="Cita"/> aggregate roots.
    /// </summary>
    public interface ICitaRepository : IRepository<Cita, long>
    {
    }
}
