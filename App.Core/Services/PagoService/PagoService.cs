using LegacyBarber.App.Core.Interfaces.Persistence;

namespace LegacyBarber.App.Core.Services.PagoService
{
    /// <summary>
    /// Default implementation of <see cref="IPagoService"/>.
    /// </summary>
    public sealed class PagoService : IPagoService
    {
        private readonly IPagoRepository pagoRepository;
        private readonly ICitaRepository citaRepository;
        private readonly IUnitOfWork unitOfWork;

        public PagoService(
            IPagoRepository pagoRepository,
            ICitaRepository citaRepository,
            IUnitOfWork unitOfWork)
        {
            this.pagoRepository = pagoRepository;
            this.citaRepository = citaRepository;
            this.unitOfWork = unitOfWork;
        }
    }
}
