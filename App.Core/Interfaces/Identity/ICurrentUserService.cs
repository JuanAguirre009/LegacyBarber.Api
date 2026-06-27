namespace LegacyBarber.App.Core.Interfaces.Identity
{
    /// <summary>
    /// Provides access to the currently authenticated user's identity.
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// Gets a value indicating whether the current user is authenticated.
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Gets the identifier of the current user, if authenticated.
        /// </summary>
        long? UsuarioId { get; }

        /// <summary>
        /// Gets the full name of the current user, if authenticated.
        /// </summary>
        string NombreCompleto { get; }

        /// <summary>
        /// Gets the barberia identifier of the current user, if available.
        /// </summary>
        long? BarberiaId { get; }

        /// <summary>
        /// Gets the roles of the current user.
        /// </summary>
        IReadOnlyCollection<string> Roles { get; }
    }
}
