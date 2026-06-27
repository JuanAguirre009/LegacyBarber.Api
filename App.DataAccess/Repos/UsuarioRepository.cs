using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LegacyBarber.App.DataAccess.Repos
{
    internal class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext context;

        public UsuarioRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Usuario?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await context.Usuarios
                .AsNoTracking()
                .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<Usuario?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await context.Usuarios
                .AsNoTracking()
                .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
                .FirstOrDefaultAsync(u => u.Email == email.ToLowerInvariant(), cancellationToken);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Usuarios
                .AsNoTracking()
                .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
                .ToListAsync(cancellationToken);
        }

        public async Task<Usuario> CreateAsync(Usuario user, CancellationToken cancellationToken = default)
        {
            await context.Usuarios.AddAsync(user, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return user;
        }

        public void Update(Usuario user)
        {
            Usuario existing = context.Usuarios
                .Include(u => u.UsuarioRoles)
                .First(u => u.Id == user.Id);

            existing.NombreCompleto = user.NombreCompleto;
            existing.Telefono = user.Telefono;
            existing.Email = user.Email;
            existing.PasswordHash = user.PasswordHash;
            existing.Activo = user.Activo;
            existing.ActualizadoEn = user.ActualizadoEn;

            context.UsuarioRoles.RemoveRange(existing.UsuarioRoles);
            existing.ReplaceRoles(user.Roles.Select(r => new UsuarioRol
            {
                UsuarioId = user.Id,
                RolId = context.Roles.First(x => x.Nombre == r).Id
            }).ToList());

            context.Usuarios.Update(existing);
        }

        public void Delete(Usuario user)
        {
            context.Usuarios.Remove(user);
        }
    }
}
