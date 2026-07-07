using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.DataAccess;
using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.DataAccess.Repos
{
    internal sealed class BarberoRepository : BaseRepository<Barbero, long>, IBarberoRepository
    {
        public BarberoRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
