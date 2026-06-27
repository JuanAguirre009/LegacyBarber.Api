using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LegacyBarber.App.DataAccess.Repos
{
    internal class TokenRefrescoRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext context;

        public TokenRefrescoRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<TokenRefresco?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await context.TokensRefresco
                .AsNoTracking()
                .Include(rt => rt.Usuario)
                .FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
        }

        public async Task<TokenRefresco> CreateAsync(TokenRefresco tokenRefresco, CancellationToken cancellationToken = default)
        {
            await context.TokensRefresco.AddAsync(tokenRefresco, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return tokenRefresco;
        }

        public void Update(TokenRefresco tokenRefresco)
        {
            context.TokensRefresco.Update(tokenRefresco);
        }
    }
}
