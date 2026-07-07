using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.DataAccess;
using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.DataAccess.Repos
{
    internal sealed class ServicioRepository : BaseRepository<Servicio, long>, IServicioRepository
    {
        public ServicioRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
