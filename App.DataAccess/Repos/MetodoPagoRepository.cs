using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.DataAccess;
using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.DataAccess.Repos
{
    internal sealed class MetodoPagoRepository : BaseRepository<MetodoPago, long>, IMetodoPagoRepository
    {
        public MetodoPagoRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
