using LegacyBarber.App.Core.Interfaces.Persistence;

namespace LegacyBarber.App.Core.Services.BarberiaService
{
    /// <summary>
    /// Default implementation of <see cref="IBarberiaService"/>.
    /// </summary>
    public sealed class BarberiaService : IBarberiaService
    {
        private readonly IBarberiaRepository barberiaRepository;
        private readonly IUnitOfWork unitOfWork;

        public BarberiaService(IBarberiaRepository barberiaRepository, IUnitOfWork unitOfWork)
        {
            this.barberiaRepository = barberiaRepository;
            this.unitOfWork = unitOfWork;
        }
    }
}
