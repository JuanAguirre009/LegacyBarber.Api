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

        public async Task<Usuario?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await context.Usuarios
                .AsNoTracking()
                .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public override async Task<IReadOnlyList<Usuario>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Usuarios
                .AsNoTracking()
                .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
                .ToListAsync(cancellationToken);
        }
    }
}
