namespace LegacyBarber.App.Domain.Entities
{
    public sealed class UsuarioRol
    {
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public long RolId { get; set; }
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        public Usuario Usuario { get; set; } = default!;
        public Rol Rol { get; set; } = default!;
    }
}
