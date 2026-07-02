using LegacyBarber.App.Core.Interfaces.Identity;
using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.Core.Model.Identity;
using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Services.ClienteService
{
    /// <summary>
    /// Default implementation of <see cref="IClienteService"/>.
    /// </summary>
    public sealed class ClienteService : IClienteService
    {
        private readonly IClienteRepository clienteRepository;
        private readonly IBarberiaRepository barberiaRepository;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IAuthService authService;
        private readonly IUnitOfWork unitOfWork;

        public ClienteService(
            IClienteRepository clienteRepository,
            IBarberiaRepository barberiaRepository,
            IUsuarioRepository usuarioRepository,
            IAuthService authService,
            IUnitOfWork unitOfWork)
        {
            this.clienteRepository = clienteRepository;
            this.barberiaRepository = barberiaRepository;
            this.usuarioRepository = usuarioRepository;
            this.authService = authService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<LoginResponseModel> AsociarBarberiaAsync(long usuarioId, long barberiaId, CancellationToken cancellationToken = default)
        {
            Barberia? barberia = await barberiaRepository.GetByIdAsync(barberiaId, cancellationToken);
            if (barberia is null)
                throw new KeyNotFoundException("La barbería no existe.");

            if (!barberia.Activa || barberia.Estado != EstadoBarberia.Activa)
                throw new InvalidOperationException("La barbería no está disponible.");

            IReadOnlyList<Cliente> existing = await clienteRepository.GetFilteredAsync(
                c => c.UsuarioId == usuarioId && c.BarberiaId == barberiaId,
                cancellationToken);

            if (existing.Count > 0)
                throw new InvalidOperationException("El usuario ya está asociado a esta barbería.");

            var cliente = new Cliente
            {
                UsuarioId = usuarioId,
                BarberiaId = barberiaId
            };

            clienteRepository.Add(cliente);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            Usuario? user = await usuarioRepository.GetByIdAsync(usuarioId, cancellationToken);
            if (user is null)
                throw new InvalidOperationException("Usuario no encontrado.");

            return await authService.BuildLoginResponseAsync(user, cancellationToken);
        }
    }
}
