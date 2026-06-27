namespace LegacyBarber.App.Core.Model.Identity
{
    /// <summary>
    /// Represents an application user in the core layer.
    /// </summary>
    public class UsuarioModel
    {
        public long Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public long? BarberiaId { get; set; }
        public bool Activo { get; set; }
        public ICollection<string> Roles { get; set; } = new List<string>();
    }
}
