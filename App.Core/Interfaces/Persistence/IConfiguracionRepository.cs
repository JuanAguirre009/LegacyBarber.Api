using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    /// <summary>
    /// Provides persistence operations for <see cref="Configuracion"/> aggregate roots.
    /// </summary>
    public interface IConfiguracionRepository : IRepository<Configuracion, long>
    {
    }
}
