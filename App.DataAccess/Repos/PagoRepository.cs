using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.DataAccess;
using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.DataAccess.Repos
{
    internal sealed class PagoRepository : BaseRepository<Pago, long>, IPagoRepository
    {
        public PagoRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
