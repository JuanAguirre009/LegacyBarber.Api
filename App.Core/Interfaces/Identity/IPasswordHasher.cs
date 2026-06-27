using LegacyBarber.App.Core.Model.Identity;

namespace LegacyBarber.App.Core.Interfaces.Identity
{
    /// <summary>
    /// Provides password hashing and verification services.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hashes a plain text password.
        /// </summary>
        /// <param name="password">The plain text password.</param>
        /// <returns>The hashed password.</returns>
        string HashPassword(string password);

        /// <summary>
        /// Verifies a plain text password against a hashed password.
        /// </summary>
        /// <param name="hashedPassword">The stored hash.</param>
        /// <param name="password">The password to verify.</param>
        /// <returns><see langword="true" /> if the password matches; otherwise, <see langword="false" />.</returns>
        bool VerifyPassword(string hashedPassword, string password);
    }
}
