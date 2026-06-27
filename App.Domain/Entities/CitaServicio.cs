namespace LegacyBarber.App.Domain.Entities
{
    public sealed class CitaServicio
    {
        public long Id { get; set; }
        public long CitaId { get; set; }
        public long ServicioId { get; set; }
        public decimal PrecioAplicado { get; set; }
        public short DuracionMinutos { get; set; }
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        public Cita Cita { get; set; } = default!;
        public Servicio Servicio { get; set; } = default!;
    }
}
