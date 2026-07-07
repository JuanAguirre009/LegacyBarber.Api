using LegacyBarber.App.Core.Interfaces.Persistence;

namespace LegacyBarber.App.Core.Services.ResenaService
{
    /// <summary>
    /// Default implementation of <see cref="IResenaService"/>.
    /// </summary>
    public sealed class ResenaService : IResenaService
    {
        private readonly IResenaRepository resenaRepository;
        private readonly ICitaRepository citaRepository;
        private readonly IUnitOfWork unitOfWork;

        public ResenaService(
            IResenaRepository resenaRepository,
            ICitaRepository citaRepository,
            IUnitOfWork unitOfWork)
        {
            this.resenaRepository = resenaRepository;
            this.citaRepository = citaRepository;
            this.unitOfWork = unitOfWork;
        }
    }
}
