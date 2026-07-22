using LegacyBarber.App.Core.Model.Identity;

namespace LegacyBarber.App.Core.Services.CategoriaServicioService
{
    public interface ICategoriaServicioService
    {
        Task<IReadOnlyList<CategoriaModel>> GetAllAsync(long barberiaId, CancellationToken cancellationToken = default);
        Task<CategoriaModel> CreateAsync(long barberiaId, CrearCategoriaModel model, CancellationToken cancellationToken = default);
        Task<CategoriaModel> UpdateAsync(long barberiaId, long categoriaId, ActualizarCategoriaModel model, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(long barberiaId, long categoriaId, CancellationToken cancellationToken = default);
    }
}
