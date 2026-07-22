using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.DataAccess;
using LegacyBarber.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LegacyBarber.App.DataAccess.Repos
{
    internal sealed class ServicioRepository : BaseRepository<Servicio, long>, IServicioRepository
    {
        public ServicioRepository(AppDbContext context)
            : base(context)
        {
        }

        public override async Task<Servicio?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await context.Servicios
                .Include(s => s.Categoria)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }
    }
}
