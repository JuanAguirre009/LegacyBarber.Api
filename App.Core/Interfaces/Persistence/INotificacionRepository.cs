using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    /// <summary>
    /// Provides persistence operations for <see cref="Notificacion"/> aggregate roots.
    /// </summary>
    public interface INotificacionRepository : IRepository<Notificacion, long>
    {
    }
}
