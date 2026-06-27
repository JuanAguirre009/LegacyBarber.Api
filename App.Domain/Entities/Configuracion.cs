namespace LegacyBarber.App.Domain.Entities
{
    public sealed class Configuracion
    {
        public long Id { get; set; }
        public long BarberiaId { get; set; }
        public string Clave { get; set; } = string.Empty;
        public string Valor { get; set; } = "{}";
        public DateTime ActualizadoEn { get; set; } = DateTime.UtcNow;

        public Barberia Barberia { get; set; } = default!;
    }
}
