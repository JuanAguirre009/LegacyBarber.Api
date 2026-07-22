using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.DataAccess;
using LegacyBarber.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LegacyBarber.App.DataAccess.Repos
{
    internal sealed class BarberoRepository : BaseRepository<Barbero, long>, IBarberoRepository
    {
        public BarberoRepository(AppDbContext context)
            : base(context)
        {
        }

        public override async Task<Barbero?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await context.Barberos
                .Include(b => b.Usuario)
                .Include(b => b.BarberoServicios)
                .ThenInclude(bs => bs.Servicio)
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Barbero>> GetAllByBarberiaAsync(long barberiaId, CancellationToken cancellationToken = default)
        {
            return await context.Barberos
                .AsNoTracking()
                .Include(b => b.Usuario)
                .Include(b => b.BarberoServicios)
                .ThenInclude(bs => bs.Servicio)
                .Where(b => b.BarberiaId == barberiaId)
                .OrderBy(b => b.Usuario.NombreCompleto)
                .ToListAsync(cancellationToken);
        }
    }
}
