namespace LegacyBarber.App.Core.Model.Identity
{
    /// <summary>
    /// Data required to update an existing user.
    /// </summary>
    public class ActualizarUsuarioModel
    {
        public string NombreCompleto { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public bool Activo { get; set; }
        public ICollection<string> Roles { get; set; } = new List<string>();
    }
}
