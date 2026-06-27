namespace LegacyBarber.App.Domain.Entities
{
    /// <summary>
    /// Represents a refresh token used to obtain new access tokens without re-authenticating.
    /// </summary>
    public sealed class TokenRefresco
    {
        public long Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? RevokedAt { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsActive => RevokedAt == null && !IsExpired;

        public long UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = default!;

        public static TokenRefresco Create(string token, DateTime expiresAt)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("The refresh token value is required.", nameof(token));
            if (expiresAt <= DateTime.UtcNow)
                throw new ArgumentException("The expiration date must be in the future.", nameof(expiresAt));

            return new TokenRefresco
            {
                Token = token,
                ExpiresAt = expiresAt,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Revoke()
        {
            if (RevokedAt != null)
                throw new InvalidOperationException("The refresh token has already been revoked.");

            RevokedAt = DateTime.UtcNow;
        }
    }
}
