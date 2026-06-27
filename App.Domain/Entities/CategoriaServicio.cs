namespace LegacyBarber.App.Domain.Entities
{
    public sealed class CategoriaServicio
    {
        public long Id { get; set; }
        public long BarberiaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public short Orden { get; set; } = 0;
        public bool Activa { get; set; } = true;
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        public Barberia Barberia { get; set; } = default!;
        public ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
    }
}
