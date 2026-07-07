using LegacyBarber.App.Core.Interfaces.Persistence;

namespace LegacyBarber.App.Core.Services.CitaService
{
    /// <summary>
    /// Default implementation of <see cref="ICitaService"/>.
    /// </summary>
    public sealed class CitaService : ICitaService
    {
        private readonly ICitaRepository citaRepository;
        private readonly IBarberoRepository barberoRepository;
        private readonly IClienteRepository clienteRepository;
        private readonly IServicioRepository servicioRepository;
        private readonly IUnitOfWork unitOfWork;

        public CitaService(
            ICitaRepository citaRepository,
            IBarberoRepository barberoRepository,
            IClienteRepository clienteRepository,
            IServicioRepository servicioRepository,
            IUnitOfWork unitOfWork)
        {
            this.citaRepository = citaRepository;
            this.barberoRepository = barberoRepository;
            this.clienteRepository = clienteRepository;
            this.servicioRepository = servicioRepository;
            this.unitOfWork = unitOfWork;
        }
    }
}
