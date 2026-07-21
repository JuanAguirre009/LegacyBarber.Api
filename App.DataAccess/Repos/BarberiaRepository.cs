using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.DataAccess;
using LegacyBarber.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LegacyBarber.App.DataAccess.Repos
{
    internal sealed class BarberiaRepository : BaseRepository<Barberia, long>, IBarberiaRepository
    {
        public BarberiaRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<bool> ExisteSlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            string normalizedSlug = slug.Trim().ToLowerInvariant();
            return await context.Barberias.AnyAsync(b => b.Slug == normalizedSlug, cancellationToken);
        }

        public async Task<bool> ExisteEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            string normalizedEmail = email.Trim().ToLowerInvariant();
            return await context.Barberias.AnyAsync(b => b.Email == normalizedEmail, cancellationToken);
        }
    }
}
