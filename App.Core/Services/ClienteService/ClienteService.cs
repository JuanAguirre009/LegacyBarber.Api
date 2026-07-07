using LegacyBarber.App.Core.Interfaces.Persistence;

namespace LegacyBarber.App.Core.Services.ClienteService
{
    /// <summary>
    /// Default implementation of <see cref="IClienteService"/>.
    /// </summary>
    public sealed class ClienteService : IClienteService
    {
        private readonly IClienteRepository clienteRepository;
        private readonly IUnitOfWork unitOfWork;

        public ClienteService(IClienteRepository clienteRepository, IUnitOfWork unitOfWork)
        {
            this.clienteRepository = clienteRepository;
            this.unitOfWork = unitOfWork;
        }
    }
}
