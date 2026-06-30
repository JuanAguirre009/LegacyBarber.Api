using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.DataAccess;
using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.DataAccess.Repos
{
    internal sealed class CategoriaServicioRepository : BaseRepository<CategoriaServicio, long>, ICategoriaServicioRepository
    {
        public CategoriaServicioRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
