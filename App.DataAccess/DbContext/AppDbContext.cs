using LegacyBarber.App.Core.Interfaces.Persistence;
using LegacyBarber.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LegacyBarber.App.DataAccess
{
    /// <summary>
    /// Provides access to persistent data models and implements
    /// <see cref="IUnitOfWork" /> to coordinate transactional saves.
    /// </summary>
    public class AppDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Barberia> Barberias => Set<Barberia>();
        public DbSet<Archivo> Archivos => Set<Archivo>();
        public DbSet<Rol> Roles => Set<Rol>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<UsuarioRol> UsuarioRoles => Set<UsuarioRol>();
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Barbero> Barberos => Set<Barbero>();
        public DbSet<CategoriaServicio> CategoriasServicios => Set<CategoriaServicio>();
        public DbSet<Servicio> Servicios => Set<Servicio>();
        public DbSet<BarberoServicio> BarberoServicios => Set<BarberoServicio>();
        public DbSet<HorarioBarbero> HorariosBarberos => Set<HorarioBarbero>();
        public DbSet<ExcepcionHorario> ExcepcionesHorarios => Set<ExcepcionHorario>();
        public DbSet<EstadoCita> EstadosCita => Set<EstadoCita>();
        public DbSet<Cita> Citas => Set<Cita>();
        public DbSet<CitaServicio> CitaServicios => Set<CitaServicio>();
        public DbSet<MetodoPago> MetodosPago => Set<MetodoPago>();
        public DbSet<Pago> Pagos => Set<Pago>();
        public DbSet<Resena> Resenas => Set<Resena>();
        public DbSet<Notificacion> Notificaciones => Set<Notificacion>();
        public DbSet<Configuracion> Configuraciones => Set<Configuracion>();
        public DbSet<Auditoria> Auditorias => Set<Auditoria>();
        public DbSet<TokenRefresco> TokensRefresco => Set<TokenRefresco>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureBarberia(modelBuilder);
            ConfigureArchivo(modelBuilder);
            ConfigureRol(modelBuilder);
            ConfigureUsuario(modelBuilder);
            ConfigureUsuarioRol(modelBuilder);
            ConfigureCliente(modelBuilder);
            ConfigureBarbero(modelBuilder);
            ConfigureCategoriaServicio(modelBuilder);
            ConfigureServicio(modelBuilder);
            ConfigureBarberoServicio(modelBuilder);
            ConfigureHorarioBarbero(modelBuilder);
            ConfigureExcepcionHorario(modelBuilder);
            ConfigureEstadoCita(modelBuilder);
            ConfigureCita(modelBuilder);
            ConfigureCitaServicio(modelBuilder);
            ConfigureMetodoPago(modelBuilder);
            ConfigurePago(modelBuilder);
            ConfigureResena(modelBuilder);
            ConfigureNotificacion(modelBuilder);
            ConfigureConfiguracion(modelBuilder);
            ConfigureAuditoria(modelBuilder);
            ConfigureRefreshToken(modelBuilder);

            SeedData(modelBuilder);
        }

        private static void ConfigureBarberia(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Barberia>(entity =>
            {
                entity.HasIndex(b => b.Slug).IsUnique();
                entity.HasIndex(b => b.Email).IsUnique();
                entity.Property(b => b.HorarioAtencion).HasColumnType("jsonb").HasDefaultValue("{}");
                entity.Property(b => b.Configuracion).HasColumnType("jsonb").HasDefaultValue("{}");
                entity.Property(b => b.Estado).HasMaxLength(50).HasDefaultValue(EstadoBarberia.EnConfiguracion);

                entity.HasOne(b => b.Logo).WithMany().HasForeignKey(b => b.LogoId).OnDelete(DeleteBehavior.SetNull);
            });
        }

        private static void ConfigureArchivo(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Archivo>(entity =>
            {
                entity.HasOne(a => a.Barberia).WithMany(b => b.Archivos).HasForeignKey(a => a.BarberiaId).OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static void ConfigureRol(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasIndex(r => r.Nombre).IsUnique();
            });
        }

        private static void ConfigureUsuario(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Email).HasColumnType("citext");

                entity.HasOne(u => u.Barberia).WithMany(b => b.Usuarios).HasForeignKey(u => u.BarberiaId).OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(u => u.Foto).WithMany().HasForeignKey(u => u.FotoId).OnDelete(DeleteBehavior.SetNull);
            });
        }

        private static void ConfigureUsuarioRol(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioRol>(entity =>
            {
                entity.HasIndex(ur => new { ur.UsuarioId, ur.RolId }).IsUnique();

                entity.HasOne(ur => ur.Usuario).WithMany(u => u.UsuarioRoles).HasForeignKey(ur => ur.UsuarioId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(ur => ur.Rol).WithMany(r => r.UsuarioRoles).HasForeignKey(ur => ur.RolId).OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static void ConfigureCliente(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasIndex(c => new { c.BarberiaId, c.UsuarioId }).IsUnique();
                entity.Property(c => c.Preferencias).HasColumnType("jsonb").HasDefaultValue("{}");

                entity.HasOne(c => c.Usuario).WithMany(u => u.Clientes).HasForeignKey(c => c.UsuarioId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(c => c.Barberia).WithMany(b => b.Clientes).HasForeignKey(c => c.BarberiaId).OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static void ConfigureBarbero(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Barbero>(entity =>
            {
                entity.HasIndex(b => b.UsuarioId).IsUnique();
                entity.Property(b => b.Especialidades).HasColumnType("jsonb").HasDefaultValue("[]");

                entity.HasOne(b => b.Usuario).WithOne(u => u.Barbero).HasForeignKey<Barbero>(b => b.UsuarioId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(b => b.Barberia).WithMany(ba => ba.Barberos).HasForeignKey(b => b.BarberiaId).OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static void ConfigureCategoriaServicio(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaServicio>(entity =>
            {
                entity.HasIndex(c => new { c.BarberiaId, c.Nombre }).IsUnique();

                entity.HasOne(c => c.Barberia).WithMany(b => b.CategoriasServicios).HasForeignKey(c => c.BarberiaId).OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static void ConfigureServicio(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.HasIndex(s => new { s.BarberiaId, s.Nombre }).IsUnique();

                entity.HasOne(s => s.Barberia).WithMany(b => b.Servicios).HasForeignKey(s => s.BarberiaId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(s => s.Categoria).WithMany(c => c.Servicios).HasForeignKey(s => s.CategoriaId).OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(s => s.Imagen).WithMany().HasForeignKey(s => s.ImagenId).OnDelete(DeleteBehavior.SetNull);
            });
        }

        private static void ConfigureBarberoServicio(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BarberoServicio>(entity =>
            {
                entity.HasIndex(bs => new { bs.BarberoId, bs.ServicioId }).IsUnique();

                entity.HasOne(bs => bs.Barbero).WithMany(b => b.BarberoServicios).HasForeignKey(bs => bs.BarberoId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(bs => bs.Servicio).WithMany(s => s.BarberoServicios).HasForeignKey(bs => bs.ServicioId).OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static void ConfigureHorarioBarbero(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HorarioBarbero>(entity =>
            {
                entity.HasIndex(h => new { h.BarberoId, h.DiaSemana, h.HoraInicio }).IsUnique();

                entity.HasOne(h => h.Barbero).WithMany(b => b.Horarios).HasForeignKey(h => h.BarberoId).OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static void ConfigureExcepcionHorario(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExcepcionHorario>(entity =>
            {
                entity.HasIndex(e => new { e.BarberoId, e.Fecha }).IsUnique();

                entity.HasOne(e => e.Barbero).WithMany(b => b.ExcepcionesHorarios).HasForeignKey(e => e.BarberoId).OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static void ConfigureEstadoCita(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EstadoCita>(entity =>
            {
                entity.HasIndex(e => e.Nombre).IsUnique();
            });
        }

        private static void ConfigureCita(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cita>(entity =>
            {
                entity.HasCheckConstraint("chk_cita_horas", "hora_inicio < hora_fin");
                entity.HasIndex(c => new { c.BarberoId, c.Fecha, c.HoraInicio });
                entity.HasIndex(c => new { c.ClienteId, c.Fecha });

                entity.HasOne(c => c.Barberia).WithMany(b => b.Citas).HasForeignKey(c => c.BarberiaId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(c => c.Cliente).WithMany(cl => cl.Citas).HasForeignKey(c => c.ClienteId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(c => c.Barbero).WithMany(b => b.Citas).HasForeignKey(c => c.BarberoId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(c => c.EstadoCita).WithMany(e => e.Citas).HasForeignKey(c => c.EstadoCitaId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(c => c.CitaOriginal).WithMany(c => c.Reprogramaciones).HasForeignKey(c => c.CitaOriginalId).OnDelete(DeleteBehavior.SetNull);
            });
        }

        private static void ConfigureCitaServicio(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CitaServicio>(entity =>
            {
                entity.HasIndex(cs => new { cs.CitaId, cs.ServicioId }).IsUnique();

                entity.HasOne(cs => cs.Cita).WithMany(c => c.CitaServicios).HasForeignKey(cs => cs.CitaId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(cs => cs.Servicio).WithMany(s => s.CitaServicios).HasForeignKey(cs => cs.ServicioId).OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void ConfigureMetodoPago(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MetodoPago>(entity =>
            {
                entity.HasIndex(m => new { m.BarberiaId, m.Codigo }).IsUnique();
                entity.Property(m => m.Configuracion).HasColumnType("jsonb").HasDefaultValue("{}");

                entity.HasOne(m => m.Barberia).WithMany(b => b.MetodosPago).HasForeignKey(m => m.BarberiaId).OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static void ConfigurePago(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pago>(entity =>
            {
                entity.Property(p => p.DatosRespuesta).HasColumnType("jsonb").HasDefaultValue("{}");

                entity.HasOne(p => p.Barberia).WithMany(b => b.Pagos).HasForeignKey(p => p.BarberiaId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(p => p.Cita).WithMany(c => c.Pagos).HasForeignKey(p => p.CitaId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(p => p.Cliente).WithMany(c => c.Pagos).HasForeignKey(p => p.ClienteId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(p => p.MetodoPago).WithMany(m => m.Pagos).HasForeignKey(p => p.MetodoPagoId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(p => p.UsuarioRegistrador).WithMany().HasForeignKey(p => p.RegistradoPor).OnDelete(DeleteBehavior.SetNull);
            });
        }

        private static void ConfigureResena(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resena>(entity =>
            {
                entity.HasIndex(r => r.CitaId).IsUnique();

                entity.HasOne(r => r.Cita).WithOne(c => c.Resena).HasForeignKey<Resena>(r => r.CitaId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(r => r.Cliente).WithMany(c => c.Resenas).HasForeignKey(r => r.ClienteId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(r => r.Barbero).WithMany(b => b.Resenas).HasForeignKey(r => r.BarberoId).OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void ConfigureNotificacion(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notificacion>(entity =>
            {
                entity.Property(n => n.Datos).HasColumnType("jsonb").HasDefaultValue("{}");
                entity.HasIndex(n => new { n.UsuarioId, n.Estado }).HasFilter("\"estado\" = 'pendiente'");

                entity.HasOne(n => n.Barberia).WithMany(b => b.Notificaciones).HasForeignKey(n => n.BarberiaId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(n => n.Usuario).WithMany().HasForeignKey(n => n.UsuarioId).OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static void ConfigureConfiguracion(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Configuracion>(entity =>
            {
                entity.HasIndex(c => new { c.BarberiaId, c.Clave }).IsUnique();
                entity.Property(c => c.Valor).HasColumnType("jsonb");

                entity.HasOne(c => c.Barberia).WithMany(b => b.Configuraciones).HasForeignKey(c => c.BarberiaId).OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static void ConfigureAuditoria(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auditoria>(entity =>
            {
                entity.Property(a => a.DatosAnteriores).HasColumnType("jsonb");
                entity.Property(a => a.DatosNuevos).HasColumnType("jsonb");

                entity.HasOne(a => a.Barberia).WithMany(b => b.Auditorias).HasForeignKey(a => a.BarberiaId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(a => a.Usuario).WithMany().HasForeignKey(a => a.UsuarioId).OnDelete(DeleteBehavior.SetNull);
            });
        }

        private static void ConfigureRefreshToken(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TokenRefresco>(entity =>
            {
                entity.HasIndex(rt => rt.Token).IsUnique();
                entity.HasIndex(rt => rt.UsuarioId);

                entity.HasOne(rt => rt.Usuario).WithMany(u => u.TokensRefresco).HasForeignKey(rt => rt.UsuarioId).OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, Nombre = "superadmin", Descripcion = "Superadministrador del sistema Legacy Barber", EsSistema = true },
                new Rol { Id = 2, Nombre = "admin", Descripcion = "Administrador de la barbería", EsSistema = true },
                new Rol { Id = 3, Nombre = "barbero", Descripcion = "Barbero que atiende citas", EsSistema = true },
                new Rol { Id = 4, Nombre = "cliente", Descripcion = "Cliente que agenda citas", EsSistema = true }
            );

            modelBuilder.Entity<EstadoCita>().HasData(
                new EstadoCita { Id = 1, Nombre = "pendiente", Descripcion = "Cita creada, esperando pago o confirmación", Color = "#D3B40E" },
                new EstadoCita { Id = 2, Nombre = "confirmada", Descripcion = "Cita confirmada", Color = "#28c840" },
                new EstadoCita { Id = 3, Nombre = "en_curso", Descripcion = "Cliente está siendo atendido", Color = "#D3B40E" },
                new EstadoCita { Id = 4, Nombre = "completada", Descripcion = "Servicio finalizado", Color = "#28c840" },
                new EstadoCita { Id = 5, Nombre = "cancelada_por_cliente", Descripcion = "Cancelada por el cliente", Color = "#ff5a5a" },
                new EstadoCita { Id = 6, Nombre = "cancelada_por_barbero", Descripcion = "Cancelada por el barbero o admin", Color = "#ff5a5a" },
                new EstadoCita { Id = 7, Nombre = "cancelada_por_sistema", Descripcion = "Cancelada por no pago u otra regla", Color = "#ff5a5a" },
                new EstadoCita { Id = 8, Nombre = "no_show", Descripcion = "Cliente no asistió", Color = "#ff5a5a" },
                new EstadoCita { Id = 9, Nombre = "reprogramada", Descripcion = "Cita reprogramada, reemplazada por otra", Color = "#D3B40E" }
            );
        }
    }
}
