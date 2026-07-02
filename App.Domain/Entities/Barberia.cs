namespace LegacyBarber.App.Domain.Entities
{
    public sealed class Barberia
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Nit { get; set; }
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
        public string? Telefono { get; set; }
        public string Email { get; set; } = string.Empty;
        public string HorarioAtencion { get; set; } = "{}";
        public long? LogoId { get; set; }
        public string Configuracion { get; set; } = "{}";
        public bool Activa { get; set; } = true;
        public string Estado { get; set; } = EstadoBarberia.EnConfiguracion;
        public DateTime? FechaActivacion { get; set; }
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;
        public DateTime? ActualizadoEn { get; set; }

        public Archivo? Logo { get; set; }
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public ICollection<Archivo> Archivos { get; set; } = new List<Archivo>();
        public ICollection<CategoriaServicio> CategoriasServicios { get; set; } = new List<CategoriaServicio>();
        public ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
        public ICollection<Barbero> Barberos { get; set; } = new List<Barbero>();
        public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
        public ICollection<MetodoPago> MetodosPago { get; set; } = new List<MetodoPago>();
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
        public ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
        public ICollection<Configuracion> Configuraciones { get; set; } = new List<Configuracion>();
        public ICollection<Auditoria> Auditorias { get; set; } = new List<Auditoria>();
    }
}
