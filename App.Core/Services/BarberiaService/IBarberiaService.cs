using LegacyBarber.App.Core.Model.Identity;

namespace LegacyBarber.App.Core.Services.BarberiaService
{
    /// <summary>
    /// Application service for barbershop management.
    /// </summary>
    public interface IBarberiaService
    {
        /// <summary>
        /// Gets active barbershops available for customers to join.
        /// </summary>
        Task<IReadOnlyList<BarberiaResumenModel>> GetDisponiblesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new barbershop and promotes the calling user to its admin.
        /// </summary>
        Task<LoginResponseModel> CrearAsync(long usuarioId, CrearBarberiaModel model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the full data of the barbershop that the current user manages or works at.
        /// </summary>
        Task<BarberiaModel> GetMiaAsync(long barberiaId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the business hours of the barbershop.
        /// </summary>
        Task<BarberiaModel> ActualizarHorarioAsync(long barberiaId, ActualizarHorarioModel model, CancellationToken cancellationToken = default);
    }
}
