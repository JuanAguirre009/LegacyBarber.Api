using LegacyBarber.App.Core.Interfaces.Identity;
using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.Core.Model.Identity;
using LegacyBarber.App.Core.Security;
using LegacyBarber.App.Domain.Entities;

namespace LegacyBarber.App.Core.Services.Identity
{
    /// <summary>
    /// Default implementation of <see cref="IAuthService" />.
    /// </summary>
    public sealed class AuthService : IAuthService
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly ITokenService tokenService;
        private readonly IRefreshTokenGenerator refreshTokenGenerator;
        private readonly IUnitOfWork unitOfWork;
        private readonly JwtSettings jwtSettings;

        public AuthService(
            IUsuarioRepository usuarioRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            IRefreshTokenGenerator refreshTokenGenerator,
            IUnitOfWork unitOfWork,
            JwtSettings jwtSettings)
        {
            this.usuarioRepository = usuarioRepository;
            this.refreshTokenRepository = refreshTokenRepository;
            this.passwordHasher = passwordHasher;
            this.tokenService = tokenService;
            this.refreshTokenGenerator = refreshTokenGenerator;
            this.unitOfWork = unitOfWork;
            this.jwtSettings = jwtSettings;
        }

        public async Task<TokenPairModel> LoginAsync(LoginModel request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            Usuario? user = await usuarioRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null || !user.Activo || !passwordHasher.VerifyPassword(user.PasswordHash, request.Password))
                throw new UnauthorizedAccessException("Invalid credentials.");

            return await GenerateTokenPairAsync(MapToModel(user), cancellationToken);
        }

        public async Task<TokenPairModel> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(refreshToken);

            TokenRefresco? token = await refreshTokenRepository.GetByTokenAsync(refreshToken, cancellationToken);
            if (token == null || !token.IsActive)
                throw new UnauthorizedAccessException("Invalid refresh token.");

            token.Revoke();
            refreshTokenRepository.Update(token);

            Usuario? user = await usuarioRepository.GetByIdAsync(token.UsuarioId, cancellationToken);
            if (user == null)
                throw new UnauthorizedAccessException("User not found.");

            TokenPairModel tokenPair = await GenerateTokenPairAsync(MapToModel(user), cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return tokenPair;
        }

        private async Task<TokenPairModel> GenerateTokenPairAsync(UsuarioModel userModel, CancellationToken cancellationToken)
        {
            TokenPairModel tokenPair = tokenService.GenerateAccessToken(userModel);
            string refreshTokenValue = refreshTokenGenerator.Generate();
            DateTime refreshTokenExpiresAt = DateTime.UtcNow.AddDays(jwtSettings.RefreshTokenExpirationDays);

            TokenRefresco newRefreshToken = TokenRefresco.Create(refreshTokenValue, refreshTokenExpiresAt);
            newRefreshToken.UsuarioId = userModel.Id;

            await refreshTokenRepository.CreateAsync(newRefreshToken, cancellationToken);
            tokenPair.RefreshToken = refreshTokenValue;

            return tokenPair;
        }

        private static UsuarioModel MapToModel(Usuario user)
        {
            return new UsuarioModel
            {
                Id = user.Id,
                Email = user.Email,
                NombreCompleto = user.NombreCompleto,
                Telefono = user.Telefono,
                BarberiaId = user.BarberiaId,
                Activo = user.Activo,
                Roles = user.Roles.ToList()
            };
        }
    }
}
