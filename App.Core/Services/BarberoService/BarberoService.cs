using LegacyBarber.App.Core.Interfaces.Identity;
using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.Core.Model.Identity;
using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Services.BarberoService
{
    public sealed class BarberoService : IBarberoService
    {
        private readonly IBarberoRepository barberoRepository;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IRolRepository rolRepository;
        private readonly IServicioRepository servicioRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUnitOfWork unitOfWork;

        public BarberoService(
            IBarberoRepository barberoRepository,
            IUsuarioRepository usuarioRepository,
            IRolRepository rolRepository,
            IServicioRepository servicioRepository,
            IPasswordHasher passwordHasher,
            IUnitOfWork unitOfWork)
        {
            this.barberoRepository = barberoRepository;
            this.usuarioRepository = usuarioRepository;
            this.rolRepository = rolRepository;
            this.servicioRepository = servicioRepository;
            this.passwordHasher = passwordHasher;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<BarberoModel>> GetAllAsync(long barberiaId, CancellationToken cancellationToken = default)
        {
            IReadOnlyList<Barbero> barberos = await barberoRepository.GetAllByBarberiaAsync(barberiaId, cancellationToken);
            return barberos.Select(MapToModel).ToList();
        }

        public async Task<BarberoModel> GetByIdAsync(long barberiaId, long barberoId, CancellationToken cancellationToken = default)
        {
            Barbero? barbero = await barberoRepository.GetByIdAsync(barberoId, cancellationToken);
            if (barbero is null || barbero.BarberiaId != barberiaId)
                throw new KeyNotFoundException("El barbero no existe.");

            return MapToModel(barbero);
        }

        public async Task<BarberoModel> CreateAsync(long barberiaId, CrearBarberoModel model, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(model);

            Usuario? existing = await usuarioRepository.GetByEmailAsync(model.Email, cancellationToken);
            if (existing is not null)
                throw new InvalidOperationException("El email ya está registrado.");

            IReadOnlyList<Servicio> servicios = new List<Servicio>();

            if (model.ServicioIds.Count > 0)
                servicios = await ValidarServiciosPertenecenAsync(barberiaId, model.ServicioIds, cancellationToken);

            Rol? rolBarbero = await rolRepository.GetByNameAsync("barbero", cancellationToken);
            if (rolBarbero is null)
                throw new InvalidOperationException("Rol barbero no encontrado.");

            string passwordHash = passwordHasher.HashPassword(model.Password);

            Usuario usuario = Usuario.Create(
                model.Email,
                passwordHash,
                model.NombreCompleto,
                model.Telefono,
                barberiaId);

            usuario.AddRole(rolBarbero);
            usuarioRepository.Add(usuario);

            var barbero = new Barbero
            {
                BarberiaId = barberiaId,
                UsuarioId = usuario.Id,
                Usuario = usuario,
                Biografia = model.Biografia?.Trim(),
                Especialidades = string.IsNullOrWhiteSpace(model.Especialidades) ? "[]" : model.Especialidades.Trim(),
                AniosExperiencia = model.AniosExperiencia,
                ComisionPorcentaje = model.ComisionPorcentaje
            };

            barberoRepository.Add(barbero);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            if (model.ServicioIds.Count > 0)
            {
                foreach (Servicio servicio in servicios)
                {
                    var barberoServicio = new BarberoServicio
                    {
                        BarberoId = barbero.Id,
                        ServicioId = servicio.Id,
                        Servicio = servicio
                    };
                    barbero.BarberoServicios.Add(barberoServicio);
                }

                barberoRepository.Update(barbero);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return MapToModel(barbero);
        }

        public async Task<BarberoModel> UpdateAsync(long barberiaId, long barberoId, ActualizarBarberoModel model, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(model);

            Barbero? barbero = await barberoRepository.GetByIdAsync(barberoId, cancellationToken);
            if (barbero is null || barbero.BarberiaId != barberiaId)
                throw new KeyNotFoundException("El barbero no existe.");

            barbero.Usuario.Update(model.NombreCompleto, model.Telefono, model.Activo);
            barbero.Biografia = model.Biografia?.Trim();
            barbero.Especialidades = string.IsNullOrWhiteSpace(model.Especialidades) ? "[]" : model.Especialidades.Trim();
            barbero.AniosExperiencia = model.AniosExperiencia;
            barbero.ComisionPorcentaje = model.ComisionPorcentaje;
            barbero.Activo = model.Activo;

            barberoRepository.Update(barbero);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return MapToModel(barbero);
        }

        public async Task<bool> DeleteAsync(long barberiaId, long barberoId, CancellationToken cancellationToken = default)
        {
            Barbero? barbero = await barberoRepository.GetByIdAsync(barberoId, cancellationToken);
            if (barbero is null || barbero.BarberiaId != barberiaId)
                return false;

            barbero.Activo = false;
            barbero.Usuario.Activo = false;

            barberoRepository.Update(barbero);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        private async Task<IReadOnlyList<Servicio>> ValidarServiciosPertenecenAsync(long barberiaId, ICollection<long> servicioIds, CancellationToken cancellationToken)
        {
            IReadOnlyList<Servicio> servicios = await servicioRepository.GetFilteredAsync(
                s => s.BarberiaId == barberiaId && servicioIds.Contains(s.Id),
                cancellationToken);

            if (servicios.Count != servicioIds.Count)
                throw new InvalidOperationException("Uno o más servicios no pertenecen a tu barbería.");

            return servicios;
        }

        private static BarberoModel MapToModel(Barbero barbero)
        {
            return new BarberoModel
            {
                Id = barbero.Id,
                UsuarioId = barbero.UsuarioId,
                Email = barbero.Usuario.Email,
                NombreCompleto = barbero.Usuario.NombreCompleto,
                Telefono = barbero.Usuario.Telefono,
                Biografia = barbero.Biografia,
                Especialidades = barbero.Especialidades,
                AniosExperiencia = barbero.AniosExperiencia,
                ComisionPorcentaje = barbero.ComisionPorcentaje,
                CalificacionPromedio = barbero.CalificacionPromedio,
                TotalResenas = barbero.TotalResenas,
                Activo = barbero.Activo,
                Servicios = barbero.BarberoServicios
                    .Where(bs => bs.Servicio != null)
                    .Select(bs => new BarberoResumenModel
                    {
                        ServicioId = bs.ServicioId,
                        Nombre = bs.Servicio!.Nombre,
                        DuracionMinutos = bs.Servicio.DuracionMinutos,
                        Precio = bs.Servicio.Precio
                    })
                    .ToList()
            };
        }
    }
}
