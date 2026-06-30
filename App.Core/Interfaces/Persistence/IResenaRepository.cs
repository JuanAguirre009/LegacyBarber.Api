using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    /// <summary>
    /// Provides persistence operations for <see cref="Resena"/> aggregate roots.
    /// </summary>
    public interface IResenaRepository : IRepository<Resena, long>
    {
    }
}
