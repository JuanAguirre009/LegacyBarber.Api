using LegacyBarber.App.Core.Interfaces.Identity;
using LegacyBarber.App.DataAccess;
using LegacyBarber.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LegacyBarber.App.Api.Data
{
    /// <summary>
    /// Seeds initial security data into the database when the application starts.
    /// </summary>
    public sealed class DatabaseSeeder
    {
        private readonly AppDbContext context;
        private readonly IPasswordHasher passwordHasher;
        private readonly ILogger<DatabaseSeeder> logger;
        private readonly string superAdminPassword;

        public DatabaseSeeder(
            AppDbContext context,
            IPasswordHasher passwordHasher,
            ILogger<DatabaseSeeder> logger,
            IConfiguration configuration)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
            this.logger = logger;
            superAdminPassword = configuration["SuperAdmin:Password"]
                ?? throw new InvalidOperationException("SuperAdmin:Password must be configured via User Secrets or environment variable.");
        }

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            await context.Database.MigrateAsync(cancellationToken);

            if (await context.Usuarios.AnyAsync(cancellationToken))
            {
                logger.LogInformation("Database already seeded.");
                return;
            }

            logger.LogInformation("Seeding superadmin user...");

            await SeedSuperAdminAsync(cancellationToken);

            logger.LogInformation("Database seeding completed.");
        }

        private async Task SeedSuperAdminAsync(CancellationToken cancellationToken)
        {
            Rol? rolSuperAdmin = await context.Roles
                .FirstOrDefaultAsync(r => r.Nombre == "superadmin", cancellationToken);

            if (rolSuperAdmin is null)
            {
                logger.LogError("Rol superadmin not found. Database seed data may be missing.");
                return;
            }

            var superAdmin = Usuario.Create(
                "admin@legacybarber.app",
                passwordHasher.HashPassword(superAdminPassword),
                "Administrador del Sistema",
                barberiaId: null);

            superAdmin.AddRole(rolSuperAdmin);

            await context.Usuarios.AddAsync(superAdmin, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
