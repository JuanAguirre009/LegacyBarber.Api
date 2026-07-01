using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.DataAccess;
using LegacyBarber.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LegacyBarber.App.DataAccess.Repos
{
    internal sealed class RolRepository : BaseRepository<Rol, long>, IRolRepository
    {
        public RolRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<Rol?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await context.Roles
                .FirstOrDefaultAsync(r => r.Nombre == name, cancellationToken);
        }

        public async Task<IReadOnlyList<Rol>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken = default)
        {
            List<string> normalizedNames = names.Select(n => n.Trim()).ToList();
            return await context.Roles
                .Where(r => normalizedNames.Contains(r.Nombre))
                .ToListAsync(cancellationToken);
        }
    }
}
