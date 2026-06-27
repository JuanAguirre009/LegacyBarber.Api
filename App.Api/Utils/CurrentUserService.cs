using LegacyBarber.App.Core.Interfaces.Identity;
using System.Security.Claims;

namespace LegacyBarber.App.Api.Utils
{
    /// <summary>
    /// Provides access to the currently authenticated user's identity from the HTTP context.
    /// </summary>
    public sealed class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated => User.Identity?.IsAuthenticated ?? false;

        public long? UsuarioId => long.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out long id) ? id : null;

        public string NombreCompleto => User.Identity?.Name ?? "Anonymous";

        public long? BarberiaId => long.TryParse(User.FindFirst("barberia_id")?.Value, out long id) ? id : null;

        public IReadOnlyCollection<string> Roles => User.FindAll(ClaimTypes.Role)
            .Select(c => c.Value)
            .Distinct()
            .ToList()
            .AsReadOnly();

        private ClaimsPrincipal User => httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
    }
}
