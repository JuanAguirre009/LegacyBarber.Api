namespace LegacyBarber.App.Domain.Entities
{
    public sealed class Rol
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool EsSistema { get; set; } = false;
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
    }
}
