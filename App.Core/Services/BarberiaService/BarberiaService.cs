using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.Core.Model.Identity;
using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Services.BarberiaService
{
    /// <summary>
    /// Default implementation of <see cref="IBarberiaService"/>.
    /// </summary>
    public sealed class BarberiaService : IBarberiaService
    {
        private readonly IBarberiaRepository barberiaRepository;

        public BarberiaService(IBarberiaRepository barberiaRepository)
        {
            this.barberiaRepository = barberiaRepository;
        }

        public async Task<IReadOnlyList<BarberiaResumenModel>> GetDisponiblesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<Barberia> barberias = await barberiaRepository.GetFilteredAsync(
                b => b.Activa && b.Estado == EstadoBarberia.Activa,
                cancellationToken);

            return barberias
                .Select(b => new BarberiaResumenModel
                {
                    Id = b.Id,
                    Nombre = b.Nombre,
                    Slug = b.Slug,
                    Direccion = b.Direccion,
                    Ciudad = b.Ciudad,
                    Telefono = b.Telefono,
                    HorarioAtencion = b.HorarioAtencion,
                    LogoId = b.LogoId
                })
                .ToList();
        }
    }
}
