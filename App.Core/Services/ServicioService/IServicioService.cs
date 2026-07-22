using LegacyBarber.App.Core.Model.Identity;

namespace LegacyBarber.App.Core.Services.ServicioService
{
    public interface IServicioService
    {
        Task<IReadOnlyList<ServicioModel>> GetAllAsync(long barberiaId, long? categoriaId, CancellationToken cancellationToken = default);
        Task<ServicioModel> GetByIdAsync(long barberiaId, long servicioId, CancellationToken cancellationToken = default);
        Task<ServicioModel> CreateAsync(long barberiaId, CrearServicioModel model, CancellationToken cancellationToken = default);
        Task<ServicioModel> UpdateAsync(long barberiaId, long servicioId, ActualizarServicioModel model, CancellationToken cancellationToken = default);
        Task<ServicioModel> ToggleActivoAsync(long barberiaId, long servicioId, bool activo, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(long barberiaId, long servicioId, CancellationToken cancellationToken = default);
    }
}
