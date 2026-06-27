namespace LegacyBarber.App.Domain.Entities
{
    public sealed class Auditoria
    {
        public long Id { get; set; }
        public long BarberiaId { get; set; }
        public string Tabla { get; set; } = string.Empty;
        public long RegistroId { get; set; }
        public string Accion { get; set; } = string.Empty;
        public string? DatosAnteriores { get; set; }
        public string? DatosNuevos { get; set; }
        public long? UsuarioId { get; set; }
        public string? IpAddress { get; set; }
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        public Barberia Barberia { get; set; } = default!;
        public Usuario? Usuario { get; set; }
    }
}
