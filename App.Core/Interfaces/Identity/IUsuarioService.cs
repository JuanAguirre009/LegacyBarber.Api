using LegacyBarber.App.Core.Model.Identity;

namespace LegacyBarber.App.Core.Interfaces.Identity
{
    /// <summary>
    /// Manages user accounts and their roles.
    /// </summary>
    public interface IUsuarioService
    {
        Task<UsuarioModel?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<UsuarioModel?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<IEnumerable<UsuarioModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<UsuarioModel> CreateAsync(CrearUsuarioModel model, CancellationToken cancellationToken = default);
        Task<UsuarioModel?> UpdateAsync(long id, ActualizarUsuarioModel model, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}
