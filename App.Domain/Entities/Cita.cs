namespace LegacyBarber.App.Domain.Entities
{
    public sealed class Cita
    {
        public long Id { get; set; }
        public long BarberiaId { get; set; }
        public long ClienteId { get; set; }
        public long BarberoId { get; set; }
        public long EstadoCitaId { get; set; }
        public long? CitaOriginalId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public short DuracionTotalMinutos { get; set; }
        public decimal PrecioTotal { get; set; }
        public string? Notas { get; set; }
        public string Origen { get; set; } = "web";
        public string? MotivoCancelacion { get; set; }
        public long? CanceladoPor { get; set; }
        public bool Pagado { get; set; } = false;
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;
        public DateTime? ActualizadoEn { get; set; }

        public Barberia Barberia { get; set; } = default!;
        public Cliente Cliente { get; set; } = default!;
        public Barbero Barbero { get; set; } = default!;
        public EstadoCita EstadoCita { get; set; } = default!;
        public Cita? CitaOriginal { get; set; }
        public ICollection<Cita> Reprogramaciones { get; set; } = new List<Cita>();
        public ICollection<CitaServicio> CitaServicios { get; set; } = new List<CitaServicio>();
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
        public Resena? Resena { get; set; }
    }
}
