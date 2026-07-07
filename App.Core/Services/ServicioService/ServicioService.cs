using LegacyBarber.App.Core.Interfaces.Persistence;

namespace LegacyBarber.App.Core.Services.ServicioService
{
    /// <summary>
    /// Default implementation of <see cref="IServicioService"/>.
    /// </summary>
    public sealed class ServicioService : IServicioService
    {
        private readonly IServicioRepository servicioRepository;
        private readonly ICategoriaServicioRepository categoriaServicioRepository;
        private readonly IUnitOfWork unitOfWork;

        public ServicioService(
            IServicioRepository servicioRepository,
            ICategoriaServicioRepository categoriaServicioRepository,
            IUnitOfWork unitOfWork)
        {
            this.servicioRepository = servicioRepository;
            this.categoriaServicioRepository = categoriaServicioRepository;
            this.unitOfWork = unitOfWork;
        }
    }
}
