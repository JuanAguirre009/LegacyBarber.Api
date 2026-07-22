using LegacyBarber.App.Core.Model.Identity;

namespace LegacyBarber.App.Core.Services.BarberoService
{
    public interface IBarberoService
    {
        Task<IReadOnlyList<BarberoModel>> GetAllAsync(long barberiaId, CancellationToken cancellationToken = default);
        Task<BarberoModel> GetByIdAsync(long barberiaId, long barberoId, CancellationToken cancellationToken = default);
        Task<BarberoModel> CreateAsync(long barberiaId, CrearBarberoModel model, CancellationToken cancellationToken = default);
        Task<BarberoModel> UpdateAsync(long barberiaId, long barberoId, ActualizarBarberoModel model, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(long barberiaId, long barberoId, CancellationToken cancellationToken = default);
    }
}
