namespace LegacyBarber.App.Core.Model.Identity
{
    /// <summary>
    /// Data required to create a new user.
    /// </summary>
    public class CrearUsuarioModel
    {
        public string Email { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string Password { get; set; } = string.Empty;
        public long? BarberiaId { get; set; }
        public ICollection<string> Roles { get; set; } = new List<string>();
    }
}
