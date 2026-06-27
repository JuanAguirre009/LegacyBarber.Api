using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.DataAccess.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LegacyBarber.App.DataAccess
{
    public static class ServiceExtensions
    {
        public static void AddDataAccessServiceExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(op =>
            {
                op.UseNpgsql(configuration.GetConnectionString("Default"));
                op.UseSnakeCaseNamingConvention();
            });
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IRefreshTokenRepository, TokenRefrescoRepository>();
        }
    }
}
