using LegacyBarber.App.Core.Model.Identity;
using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Interfaces.Identity
{
    /// <summary>
    /// Orchestrates authentication operations such as login and refresh.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates a user with user name and password.
        /// </summary>
        /// <param name="request">The login credentials.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The authentication response including tokens and associated barbershops.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when credentials are invalid.</exception>
        Task<LoginResponseModel> LoginAsync(LoginModel request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Registers a new customer account.
        /// </summary>
        /// <param name="request">The registration data.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created user model.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the email is already in use.</exception>
        Task<UsuarioModel> RegisterAsync(RegistrarClienteModel request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Builds a full login response for an existing user, including fresh tokens.
        /// </summary>
        /// <param name="user">The authenticated user.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A login response with tokens and associated barbershops.</returns>
        Task<LoginResponseModel> BuildLoginResponseAsync(Usuario user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Exchanges a refresh token for a new access token and rotates the refresh token.
        /// </summary>
        /// <param name="TokenRefresco">The refresh token.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A new token pair.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when the refresh token is invalid.</exception>
        Task<TokenPairModel> RefreshTokenAsync(string TokenRefresco, CancellationToken cancellationToken = default);
    }
}
