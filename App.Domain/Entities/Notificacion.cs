namespace LegacyBarber.App.Domain.Entities
{
    public sealed class Notificacion
    {
        public long Id { get; set; }
        public long BarberiaId { get; set; }
        public long UsuarioId { get; set; }
        public string Canal { get; set; } = "email";
        public string Tipo { get; set; } = string.Empty;
        public string Asunto { get; set; } = string.Empty;
        public string Contenido { get; set; } = string.Empty;
        public string Datos { get; set; } = "{}";
        public DateTime? EnviadaEn { get; set; }
        public DateTime? LeidaEn { get; set; }
        public string Estado { get; set; } = "pendiente";
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        public Barberia Barberia { get; set; } = default!;
        public Usuario Usuario { get; set; } = default!;
    }
}
