namespace LegacyBarber.App.Domain.Entities
{
    public sealed class HorarioBarbero
    {
        public long Id { get; set; }
        public long BarberoId { get; set; }
        public short DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        public Barbero Barbero { get; set; } = default!;
    }
}
