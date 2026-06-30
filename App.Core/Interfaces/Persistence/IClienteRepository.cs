using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    /// <summary>
    /// Provides persistence operations for <see cref="Cliente"/> aggregate roots.
    /// </summary>
    public interface IClienteRepository : IRepository<Cliente, long>
    {
    }
}
