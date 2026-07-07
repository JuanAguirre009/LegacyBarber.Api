using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    /// <summary>
    /// Provides persistence operations for <see cref="Pago"/> aggregate roots.
    /// </summary>
    public interface IPagoRepository : IRepository<Pago, long>
    {
    }
}
