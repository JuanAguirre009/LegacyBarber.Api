using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LegacyBarber.App.DataAccess.Repos
{
    internal sealed class RolRepository : IRolRepository
    {
        private readonly AppDbContext context;

        public RolRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Rol?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Nombre == name, cancellationToken);
        }

        public async Task<IEnumerable<Rol>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken = default)
        {
            var normalizedNames = names.Select(n => n.Trim()).ToList();
            return await context.Roles
                .AsNoTracking()
                .Where(r => normalizedNames.Contains(r.Nombre))
                .ToListAsync(cancellationToken);
        }
    }
}
