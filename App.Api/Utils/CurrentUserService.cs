using LegacyBarber.App.Api.Security;
using LegacyBarber.App.Core.Interfaces.Identity;
using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.Domain.Entities;
using System.Security.Claims;

namespace LegacyBarber.App.Api.Utils
{
    /// <summary>
    /// Provides access to the currently authenticated user's identity from the HTTP context.
    /// The barbershop context is read from the <c>X-Barberia-Id</c> header when present,
    /// but it is validated against the user's allowed barbershops before being returned.
    /// </summary>
    public sealed class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUsuarioRepository usuarioRepository;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IUsuarioRepository usuarioRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.usuarioRepository = usuarioRepository;
        }

        public bool IsAuthenticated => User.Identity?.IsAuthenticated ?? false;

        public long? UsuarioId => long.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out long id) ? id : null;

        public string NombreCompleto => User.Identity?.Name ?? "Anonymous";

        public long? BarberiaId
        {
            get
            {
                long? claimBarberiaId = ReadClaimBarberiaId();
                long? headerBarberiaId = ReadHeaderBarberiaId();

                if (!headerBarberiaId.HasValue)
                    return claimBarberiaId;

                if (!UsuarioId.HasValue)
                    return claimBarberiaId;

                // Synchronous access is required by the interface; the repository call is safe
                // because ASP.NET Core does not enforce a synchronization context by default.
                Usuario? user = usuarioRepository.GetByIdAsync(UsuarioId.Value).GetAwaiter().GetResult();
                if (user == null)
                    return claimBarberiaId;

                return UserCanAccessBarberia(user, headerBarberiaId.Value)
                    ? headerBarberiaId
                    : claimBarberiaId;
            }
        }

        public IReadOnlyCollection<string> Roles => User.FindAll(ClaimTypes.Role)
            .Select(c => c.Value)
            .Distinct()
            .ToList()
            .AsReadOnly();

        private ClaimsPrincipal User => httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();

        private long? ReadHeaderBarberiaId()
        {
            HttpContext? context = httpContextAccessor.HttpContext;
            if (context != null &&
                context.Request.Headers.TryGetValue("X-Barberia-Id", out Microsoft.Extensions.Primitives.StringValues headerValue) &&
                long.TryParse(headerValue.FirstOrDefault(), out long headerBarberiaId))
            {
                return headerBarberiaId;
            }

            return null;
        }

        private long? ReadClaimBarberiaId()
        {
            return long.TryParse(User.FindFirst("barberia_id")?.Value, out long claimBarberiaId)
                ? claimBarberiaId
                : null;
        }

        private static bool UserCanAccessBarberia(Usuario user, long barberiaId)
        {
            if (user.Roles.Contains(ApplicationRoles.SuperAdmin))
                return true;

            if (user.BarberiaId == barberiaId)
                return true;

            if (user.Barbero?.BarberiaId == barberiaId)
                return true;

            return user.Clientes.Any(c => c.BarberiaId == barberiaId);
        }
    }
}
