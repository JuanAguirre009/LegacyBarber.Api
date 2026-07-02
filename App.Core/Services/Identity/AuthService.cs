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
        private readonly IRolRepository rolRepository;
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly ITokenService tokenService;
        private readonly IRefreshTokenGenerator refreshTokenGenerator;
        private readonly IUnitOfWork unitOfWork;
        private readonly JwtSettings jwtSettings;

        public AuthService(
            IUsuarioRepository usuarioRepository,
            IRolRepository rolRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            IRefreshTokenGenerator refreshTokenGenerator,
            IUnitOfWork unitOfWork,
            JwtSettings jwtSettings)
        {
            this.usuarioRepository = usuarioRepository;
            this.rolRepository = rolRepository;
            this.refreshTokenRepository = refreshTokenRepository;
            this.passwordHasher = passwordHasher;
            this.tokenService = tokenService;
            this.refreshTokenGenerator = refreshTokenGenerator;
            this.unitOfWork = unitOfWork;
            this.jwtSettings = jwtSettings;
        }

        public async Task<LoginResponseModel> LoginAsync(LoginModel request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            Usuario? user = await usuarioRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null || !user.Activo || !passwordHasher.VerifyPassword(user.PasswordHash, request.Password))
                throw new UnauthorizedAccessException("Invalid credentials.");

            return await BuildLoginResponseAsync(user, cancellationToken);
        }

        public async Task<LoginResponseModel> BuildLoginResponseAsync(Usuario user, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(user);

            UsuarioModel userModel = MapToModel(user);
            TokenPairModel tokenPair = await GenerateTokenPairAsync(userModel, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new LoginResponseModel
            {
                AccessToken = tokenPair.AccessToken,
                RefreshToken = tokenPair.RefreshToken,
                ExpiresAt = tokenPair.ExpiresAt,
                Usuario = userModel,
                Barberias = MapBarberias(user.Clientes)
            };
        }

        public async Task<UsuarioModel> RegisterAsync(RegistrarClienteModel request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            Usuario? existing = await usuarioRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existing is not null)
                throw new InvalidOperationException("El email ya está registrado.");

            Rol? rolCliente = await rolRepository.GetByNameAsync("cliente", cancellationToken);
            if (rolCliente is null)
                throw new InvalidOperationException("Rol cliente no encontrado.");

            string passwordHash = passwordHasher.HashPassword(request.Password);
            Usuario user = Usuario.Create(
                request.Email,
                passwordHash,
                request.NombreCompleto,
                request.Telefono,
                barberiaId: null);

            user.EmailVerificado = true; // Temporary until email verification is implemented.
            user.AddRole(rolCliente);

            usuarioRepository.Add(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return MapToModel(user);
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

            refreshTokenRepository.Add(newRefreshToken);
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

        private static ICollection<BarberiaResumenModel> MapBarberias(ICollection<Cliente> clientes)
        {
            return clientes
                .Where(c => c.Barberia != null)
                .Select(c => new BarberiaResumenModel
                {
                    Id = c.Barberia!.Id,
                    Nombre = c.Barberia.Nombre,
                    Slug = c.Barberia.Slug,
                    Direccion = c.Barberia.Direccion,
                    Ciudad = c.Barberia.Ciudad,
                    Telefono = c.Barberia.Telefono,
                    HorarioAtencion = c.Barberia.HorarioAtencion,
                    LogoId = c.Barberia.LogoId
                })
                .ToList();
        }
    }
}
