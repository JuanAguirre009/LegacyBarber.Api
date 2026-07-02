using LegacyBarber.App.Core.Model.Identity;

namespace LegacyBarber.App.Core.Services.ClienteService
{
    /// <summary>
    /// Application service for customer management.
    /// </summary>
    public interface IClienteService
    {
        /// <summary>
        /// Associates a user with a barbershop and returns a fresh login response.
        /// </summary>
        Task<LoginResponseModel> AsociarBarberiaAsync(long usuarioId, long barberiaId, CancellationToken cancellationToken = default);
    }
}
