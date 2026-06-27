namespace LegacyBarber.App.Domain.Entities
{
    public sealed class Archivo
    {
        public long Id { get; set; }
        public long BarberiaId { get; set; }
        public string NombreOriginal { get; set; } = string.Empty;
        public string NombreAlmacenado { get; set; } = string.Empty;
        public string Ruta { get; set; } = string.Empty;
        public string? TipoMime { get; set; }
        public long? TamanoBytes { get; set; }
        public string? EntidadTipo { get; set; }
        public long? EntidadId { get; set; }
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        public Barberia Barberia { get; set; } = default!;
    }
}
