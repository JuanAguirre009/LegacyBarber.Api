using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.DataAccess;
using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.DataAccess.Repos
{
    internal sealed class BarberiaRepository : BaseRepository<Barberia, long>, IBarberiaRepository
    {
        public BarberiaRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
