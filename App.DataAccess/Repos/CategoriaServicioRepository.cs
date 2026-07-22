using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.DataAccess;
using LegacyBarber.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LegacyBarber.App.DataAccess.Repos
{
    internal sealed class CategoriaServicioRepository : BaseRepository<CategoriaServicio, long>, ICategoriaServicioRepository
    {
        public CategoriaServicioRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IReadOnlyList<CategoriaServicio>> GetAllByBarberiaAsync(long barberiaId, CancellationToken cancellationToken = default)
        {
            return await context.CategoriasServicios
                .AsNoTracking()
                .Include(c => c.Servicios)
                .Where(c => c.BarberiaId == barberiaId)
                .OrderBy(c => c.Orden)
                .ThenBy(c => c.Nombre)
                .ToListAsync(cancellationToken);
        }
    }
}
