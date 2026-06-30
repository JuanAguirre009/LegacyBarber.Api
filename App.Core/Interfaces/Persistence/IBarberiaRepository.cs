using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    /// <summary>
    /// Provides persistence operations for <see cref="Barberia"/> aggregate roots.
    /// </summary>
    public interface IBarberiaRepository : IRepository<Barberia, long>
    {
    }
}
