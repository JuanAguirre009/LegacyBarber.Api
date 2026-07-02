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
    }
}
