namespace LegacyBarber.App.Core.Model.Identity
{
    /// <summary>
    /// Data required to register a new customer account.
    /// </summary>
    public class RegistrarClienteModel
    {
        public string NombreCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
