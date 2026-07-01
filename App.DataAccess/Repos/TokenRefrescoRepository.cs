using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.DataAccess;
using LegacyBarber.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LegacyBarber.App.DataAccess.Repos
{
    internal sealed class TokenRefrescoRepository : BaseRepository<TokenRefresco, long>, IRefreshTokenRepository
    {
        public TokenRefrescoRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<TokenRefresco?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await context.TokensRefresco
                .AsNoTracking()
                .Include(rt => rt.Usuario)
                .FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
        }
    }
}
