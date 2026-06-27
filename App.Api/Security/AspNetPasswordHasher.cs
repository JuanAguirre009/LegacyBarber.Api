using LegacyBarber.App.Core.Interfaces.Identity;
using Microsoft.AspNetCore.Identity;

namespace LegacyBarber.App.Api.Security
{
    /// <summary>
    /// Implements password hashing and verification using ASP.NET Core Identity's password hasher.
    /// </summary>
    public sealed class AspNetPasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasher<object> passwordHasher = new();

        public string HashPassword(string password)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(password);
            return passwordHasher.HashPassword(null!, password);
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(hashedPassword);
            ArgumentException.ThrowIfNullOrWhiteSpace(password);

            PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(null!, hashedPassword, password);
            return result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}
