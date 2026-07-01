using LegacyBarber.App.Api.Security;
using LegacyBarber.App.Api.Utils;
using LegacyBarber.App.Core.Interfaces.Identity;
using LegacyBarber.App.Core.Security;
using LegacyBarber.App.Core.Services.BarberiaService;
using LegacyBarber.App.Core.Services.BarberoService;
using LegacyBarber.App.Core.Services.CitaService;
using LegacyBarber.App.Core.Services.ClienteService;
using LegacyBarber.App.Core.Services.Identity;
using LegacyBarber.App.Core.Services.NotificacionService;
using LegacyBarber.App.Core.Services.PagoService;
using LegacyBarber.App.Core.Services.ResenaService;
using LegacyBarber.App.Core.Services.ServicioService;
using LegacyBarber.App.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LegacyBarber.App.Api.Extensions
{
    public static class ServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddDataAccessServiceExtensions(configuration);
            AddIdentityServices(services);
            AddBusinessServices(services);
            AddCorsPolicy(services);
        }

        public static void AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Frontend", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });
        }

        public static void AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            JwtSettings jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>() ?? new JwtSettings();

            if (string.IsNullOrWhiteSpace(jwtSettings.SecretKey) || jwtSettings.SecretKey.Length < 32 || jwtSettings.SecretKey.StartsWith("__"))
                throw new InvalidOperationException("JwtSettings:SecretKey must be configured with at least 32 characters.");

            if (string.IsNullOrWhiteSpace(jwtSettings.Issuer))
                throw new InvalidOperationException("JwtSettings:Issuer must be configured.");

            if (string.IsNullOrWhiteSpace(jwtSettings.Audience))
                throw new InvalidOperationException("JwtSettings:Audience must be configured.");

            services.AddSingleton(jwtSettings);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization();
        }

        private static void AddIdentityServices(IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher, AspNetPasswordHasher>();
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
        }

        private static void AddBusinessServices(IServiceCollection services)
        {
            services.AddScoped<IBarberiaService, BarberiaService>();
            services.AddScoped<IBarberoService, BarberoService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IServicioService, ServicioService>();
            services.AddScoped<ICitaService, CitaService>();
            services.AddScoped<IPagoService, PagoService>();
            services.AddScoped<INotificacionService, NotificacionService>();
            services.AddScoped<IResenaService, ResenaService>();
        }
    }
}
