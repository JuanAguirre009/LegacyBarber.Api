using LegacyBarber.App.Core.Interfaces.Identity;
using LegacyBarber.App.DataAccess;
using LegacyBarber.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
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

        public DatabaseSeeder(AppDbContext context, IPasswordHasher passwordHasher, ILogger<DatabaseSeeder> logger)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
            this.logger = logger;
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
            var barberia = new Barberia
            {
                Nombre = "Legacy Barber Sistema",
                Email = "admin@legacybarber.app",
                Activa = true,
                CreadoEn = DateTime.UtcNow
            };

            await context.Barberias.AddAsync(barberia, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            Rol? rolSuperAdmin = await context.Roles
                .FirstOrDefaultAsync(r => r.Nombre == "superadmin", cancellationToken);

            if (rolSuperAdmin is null)
            {
                logger.LogError("Rol superadmin not found. Database seed data may be missing.");
                return;
            }

            var superAdmin = Usuario.Create(
                "admin@legacybarber.app",
                passwordHasher.HashPassword("Admin123!"),
                "Administrador del Sistema",
                barberia.Id);

            superAdmin.AddRole(rolSuperAdmin);

            await context.Usuarios.AddAsync(superAdmin, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
