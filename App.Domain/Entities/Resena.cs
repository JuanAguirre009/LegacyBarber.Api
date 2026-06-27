namespace LegacyBarber.App.Domain.Entities
{
    public sealed class Resena
    {
        public long Id { get; set; }
        public long CitaId { get; set; }
        public long ClienteId { get; set; }
        public long BarberoId { get; set; }
        public short Calificacion { get; set; }
        public string? Comentario { get; set; }
        public bool Visible { get; set; } = true;
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        public Cita Cita { get; set; } = default!;
        public Cliente Cliente { get; set; } = default!;
        public Barbero Barbero { get; set; } = default!;
    }
}
