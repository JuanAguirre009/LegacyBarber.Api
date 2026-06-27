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
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
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
            return user;
        }

        public void Update(Usuario user)
        {
            context.Usuarios.Update(user);
        }

        public void Delete(Usuario user)
        {
            context.Usuarios.Remove(user);
        }
    }
}
