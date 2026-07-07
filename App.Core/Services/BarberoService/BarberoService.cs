using LegacyBarber.App.Core.Interfaces.Persistence;

namespace LegacyBarber.App.Core.Services.BarberoService
{
    /// <summary>
    /// Default implementation of <see cref="IBarberoService"/>.
    /// </summary>
    public sealed class BarberoService : IBarberoService
    {
        private readonly IBarberoRepository barberoRepository;
        private readonly IUnitOfWork unitOfWork;

        public BarberoService(IBarberoRepository barberoRepository, IUnitOfWork unitOfWork)
        {
            this.barberoRepository = barberoRepository;
            this.unitOfWork = unitOfWork;
        }
    }
}
