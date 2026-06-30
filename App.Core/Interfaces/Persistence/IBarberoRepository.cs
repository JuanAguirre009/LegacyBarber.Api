using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    /// <summary>
    /// Provides persistence operations for <see cref="Barbero"/> aggregate roots.
    /// </summary>
    public interface IBarberoRepository : IRepository<Barbero, long>
    {
    }
}
