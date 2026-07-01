using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Interfaces.Persistence
{
    /// <summary>
    /// Provides persistence operations for <see cref="TokenRefresco" /> entities.
    /// </summary>
    public interface IRefreshTokenRepository : IRepository<TokenRefresco, long>
    {
        Task<TokenRefresco?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
    }
}
