using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.DataAccess;
using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.DataAccess.Repos
{
    internal sealed class NotificacionRepository : BaseRepository<Notificacion, long>, INotificacionRepository
    {
        public NotificacionRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
