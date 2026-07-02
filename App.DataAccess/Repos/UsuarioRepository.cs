using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.DataAccess;
using LegacyBarber.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LegacyBarber.App.DataAccess.Repos
{
    internal sealed class UsuarioRepository : BaseRepository<Usuario, long>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context)
            : base(context)
        {
        }

        public override async Task<Usuario?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await context.Usuarios
                .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
                .Include(u => u.Clientes)
                .ThenInclude(c => c.Barberia)
                .Include(u => u.Barbero)
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<Usuario?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await context.Usuarios
                .AsNoTracking()
                .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
                .Include(u => u.Clientes)
                .ThenInclude(c => c.Barberia)
                .Include(u => u.Barbero)
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public override async Task<IReadOnlyList<Usuario>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Usuarios
                .AsNoTracking()
                .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
                .Include(u => u.Clientes)
                .ThenInclude(c => c.Barberia)
                .Include(u => u.Barbero)
                .ToListAsync(cancellationToken);
        }
    }
}
