using LegacyBarber.App.Core.Model.Identity;

namespace LegacyBarber.App.Core.Interfaces.Identity
{
    /// <summary>
    /// Generates and validates JSON Web Tokens.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates an access token for the specified user.
        /// </summary>
        /// <param name="user">The user to generate the token for.</param>
        /// <returns>A model containing the access token and its expiration.</returns>
        TokenPairModel GenerateAccessToken(UsuarioModel user);

        /// <summary>
        /// Validates an access token and extracts the user identifier.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <returns>The user identifier if valid; otherwise, <see langword="null" />.</returns>
        long? ValidateAccessToken(string token);
    }
}
