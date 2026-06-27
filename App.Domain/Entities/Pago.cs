namespace LegacyBarber.App.Domain.Entities
{
    public sealed class Pago
    {
        public long Id { get; set; }
        public long BarberiaId { get; set; }
        public long CitaId { get; set; }
        public long ClienteId { get; set; }
        public long MetodoPagoId { get; set; }
        public long? RegistradoPor { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; } = "pendiente";
        public string? ReferenciaExterna { get; set; }
        public string? TransactionIdExterno { get; set; }
        public string DatosRespuesta { get; set; } = "{}";
        public DateTime? PagadoEn { get; set; }
        public DateTime? ReembolsadoEn { get; set; }
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;
        public DateTime? ActualizadoEn { get; set; }

        public Barberia Barberia { get; set; } = default!;
        public Cita Cita { get; set; } = default!;
        public Cliente Cliente { get; set; } = default!;
        public MetodoPago MetodoPago { get; set; } = default!;
        public Usuario? UsuarioRegistrador { get; set; }
    }
}
