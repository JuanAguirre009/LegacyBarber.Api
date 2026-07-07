using LegacyBarber.App.Core.Interfaces.Identity;
using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.Core.Model.Identity;
using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Services.Identity
{
    /// <summary>
    /// Default implementation of <see cref="IUsuarioService" />.
    /// </summary>
    public sealed class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IRolRepository rolRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUnitOfWork unitOfWork;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IRolRepository rolRepository,
            IPasswordHasher passwordHasher,
            IUnitOfWork unitOfWork)
        {
            this.usuarioRepository = usuarioRepository;
            this.rolRepository = rolRepository;
            this.passwordHasher = passwordHasher;
            this.unitOfWork = unitOfWork;
        }

        public async Task<UsuarioModel?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            Usuario? user = await usuarioRepository.GetByIdAsync(id, cancellationToken);
            return user == null ? null : MapToModel(user);
        }

        public async Task<UsuarioModel?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            Usuario? user = await usuarioRepository.GetByEmailAsync(email, cancellationToken);
            return user == null ? null : MapToModel(user);
        }

        public async Task<IEnumerable<UsuarioModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<Usuario> users = await usuarioRepository.GetAllAsync(cancellationToken);
            return users.Select(MapToModel);
        }

        public async Task<UsuarioModel> CreateAsync(CrearUsuarioModel model, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(model);

            string passwordHash = passwordHasher.HashPassword(model.Password);
            Usuario user = Usuario.Create(
                model.Email,
                passwordHash,
                model.NombreCompleto,
                model.BarberiaId);

            IReadOnlyList<Rol> roles = (await rolRepository.GetByNamesAsync(model.Roles, cancellationToken)).ToList();
            user.SetRoles(roles);

            usuarioRepository.Add(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return MapToModel(user);
        }

        public async Task<UsuarioModel?> UpdateAsync(long id, ActualizarUsuarioModel model, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(model);

            Usuario? user = await usuarioRepository.GetByIdAsync(id, cancellationToken);
            if (user == null)
                return null;

            user.Update(model.NombreCompleto, model.Telefono, model.Activo);
            IReadOnlyList<Rol> roles = (await rolRepository.GetByNamesAsync(model.Roles, cancellationToken)).ToList();
            user.SetRoles(roles);
            usuarioRepository.Update(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return MapToModel(user);
        }

        public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            Usuario? user = await usuarioRepository.GetByIdAsync(id, cancellationToken);
            if (user == null)
                return false;

            usuarioRepository.Delete(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        private static UsuarioModel MapToModel(Usuario user)
        {
            return new UsuarioModel
            {
                Id = user.Id,
                Email = user.Email,
                NombreCompleto = user.NombreCompleto,
                Telefono = user.Telefono,
                BarberiaId = user.BarberiaId,
                Activo = user.Activo,
                Roles = user.Roles.ToList()
            };
        }
    }
}
