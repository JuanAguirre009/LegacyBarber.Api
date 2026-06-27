using LegacyBarber.App.Core.Interfaces.Identity;
using LegacyBarber.App.Core.Model.Identity;
using LegacyBarber.App.Core.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LegacyBarber.App.Api.Security
{
    /// <summary>
    /// Generates and validates JWT access tokens using symmetric-key signing.
    /// </summary>
    public sealed class JwtTokenService : ITokenService
    {
        private readonly JwtSettings settings;

        public JwtTokenService(IOptions<JwtSettings> options)
        {
            settings = options.Value;
        }

        public TokenPairModel GenerateAccessToken(UsuarioModel user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, user.NombreCompleto),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.NombreCompleto),
                new("barberia_id", user.BarberiaId?.ToString() ?? "")
            };

            claims.AddRange(user.Roles.Select(r => new Claim(ClaimTypes.Role, r)));

            DateTime expiresAt = DateTime.UtcNow.AddMinutes(settings.AccessTokenExpirationMinutes);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenPairModel
            {
                AccessToken = accessToken,
                ExpiresAt = expiresAt
            };
        }

        public long? ValidateAccessToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(settings.SecretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                string? usuarioId = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

                return long.TryParse(usuarioId, out long id) ? id : null;
            }
            catch
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Generates refresh tokens using a cryptographically secure random number generator.
    /// </summary>
    public sealed class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        public string Generate()
        {
            byte[] randomBytes = new byte[64];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}
