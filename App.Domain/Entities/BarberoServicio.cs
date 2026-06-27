namespace LegacyBarber.App.Domain.Entities
{
    public sealed class BarberoServicio
    {
        public long Id { get; set; }
        public long BarberoId { get; set; }
        public long ServicioId { get; set; }
        public decimal? PrecioPersonalizado { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        public Barbero Barbero { get; set; } = default!;
        public Servicio Servicio { get; set; } = default!;
    }
}
