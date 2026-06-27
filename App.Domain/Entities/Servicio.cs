namespace LegacyBarber.App.Domain.Entities
{
    public sealed class Servicio
    {
        public long Id { get; set; }
        public long BarberiaId { get; set; }
        public long? CategoriaId { get; set; }
        public long? ImagenId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public short DuracionMinutos { get; set; }
        public decimal Precio { get; set; }
        public string? Color { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;
        public DateTime? ActualizadoEn { get; set; }

        public Barberia Barberia { get; set; } = default!;
        public CategoriaServicio? Categoria { get; set; }
        public Archivo? Imagen { get; set; }
        public ICollection<BarberoServicio> BarberoServicios { get; set; } = new List<BarberoServicio>();
        public ICollection<CitaServicio> CitaServicios { get; set; } = new List<CitaServicio>();
    }
}
