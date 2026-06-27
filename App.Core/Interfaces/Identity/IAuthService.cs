using LegacyBarber.App.Core.Model.Identity;

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
        /// <returns>The token pair if authentication succeeds.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when credentials are invalid.</exception>
        Task<TokenPairModel> LoginAsync(LoginModel request, CancellationToken cancellationToken = default);

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
