namespace LegacyBarber.App.Domain.Entities
{
    public sealed class ExcepcionHorario
    {
        public long Id { get; set; }
        public long BarberoId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan? HoraInicio { get; set; }
        public TimeSpan? HoraFin { get; set; }
        public bool EstaDisponible { get; set; } = false;
        public string? Motivo { get; set; }

        public Barbero Barbero { get; set; } = default!;
    }
}
