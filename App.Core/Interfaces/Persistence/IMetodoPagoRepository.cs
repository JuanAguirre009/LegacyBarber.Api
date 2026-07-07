using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    /// <summary>
    /// Provides persistence operations for <see cref="MetodoPago"/> aggregate roots.
    /// </summary>
    public interface IMetodoPagoRepository : IRepository<MetodoPago, long>
    {
    }
}
