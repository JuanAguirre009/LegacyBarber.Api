using LegacyBarber.App.Core.Interfaces.Identity;
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
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IRolRepository rolRepository;
        private readonly IAuthService authService;
        private readonly IUnitOfWork unitOfWork;

        public BarberiaService(
            IBarberiaRepository barberiaRepository,
            IUsuarioRepository usuarioRepository,
            IRolRepository rolRepository,
            IAuthService authService,
            IUnitOfWork unitOfWork)
        {
            this.barberiaRepository = barberiaRepository;
            this.usuarioRepository = usuarioRepository;
            this.rolRepository = rolRepository;
            this.authService = authService;
            this.unitOfWork = unitOfWork;
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

        public async Task<LoginResponseModel> CrearAsync(long usuarioId, CrearBarberiaModel model, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(model);

            string slug = model.Slug.Trim().ToLowerInvariant();
            string emailContacto = model.EmailContacto.Trim().ToLowerInvariant();

            if (await barberiaRepository.ExisteSlugAsync(slug, cancellationToken))
                throw new InvalidOperationException("El slug ya está en uso.");

            if (await barberiaRepository.ExisteEmailAsync(emailContacto, cancellationToken))
                throw new InvalidOperationException("El email de contacto ya está registrado por otra barbería.");

            Usuario? user = await usuarioRepository.GetByIdAsync(usuarioId, cancellationToken);
            if (user is null)
                throw new KeyNotFoundException("El usuario no existe.");

            var barberia = new Barberia
            {
                Nombre = model.Nombre.Trim(),
                Slug = slug,
                Direccion = model.Direccion?.Trim(),
                Ciudad = model.Ciudad?.Trim(),
                Telefono = model.Telefono?.Trim(),
                Email = emailContacto,
                Activa = false,
                Estado = EstadoBarberia.EnConfiguracion,
                LogoId = model.LogoId
            };

            barberiaRepository.Add(barberia);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            user.BarberiaId = barberia.Id;

            Rol? rolAdmin = await rolRepository.GetByNameAsync("admin", cancellationToken);
            if (rolAdmin is not null)
                user.AddRole(rolAdmin);

            usuarioRepository.Update(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return await authService.BuildLoginResponseAsync(user, cancellationToken);
        }

        public async Task<BarberiaModel> GetMiaAsync(long barberiaId, CancellationToken cancellationToken = default)
        {
            Barberia? barberia = await barberiaRepository.GetByIdAsync(barberiaId, cancellationToken);
            if (barberia is null)
                throw new KeyNotFoundException("La barbería no existe.");

            return MapToModel(barberia);
        }

        public async Task<BarberiaModel> ActualizarHorarioAsync(long barberiaId, ActualizarHorarioModel model, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(model);

            Barberia? barberia = await barberiaRepository.GetByIdAsync(barberiaId, cancellationToken);
            if (barberia is null)
                throw new KeyNotFoundException("La barbería no existe.");

            barberia.HorarioAtencion = model.HorarioAtencion;
            barberiaRepository.Update(barberia);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return MapToModel(barberia);
        }

        private static BarberiaModel MapToModel(Barberia barberia)
        {
            return new BarberiaModel
            {
                Id = barberia.Id,
                Nombre = barberia.Nombre,
                Slug = barberia.Slug,
                Direccion = barberia.Direccion,
                Ciudad = barberia.Ciudad,
                Telefono = barberia.Telefono,
                Email = barberia.Email,
                HorarioAtencion = barberia.HorarioAtencion,
                LogoId = barberia.LogoId,
                Estado = barberia.Estado,
                FechaActivacion = barberia.FechaActivacion,
                CreadoEn = barberia.CreadoEn,
                ActualizadoEn = barberia.ActualizadoEn
            };
        }
    }
}
