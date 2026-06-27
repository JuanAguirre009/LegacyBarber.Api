namespace LegacyBarber.App.Core.Interfaces.Identity
{
    /// <summary>
    /// Generates cryptographically secure refresh token values.
    /// </summary>
    public interface IRefreshTokenGenerator
    {
        /// <summary>
        /// Generates a new refresh token value.
        /// </summary>
        /// <returns>A Base64-encoded random refresh token.</returns>
        string Generate();
    }
}
