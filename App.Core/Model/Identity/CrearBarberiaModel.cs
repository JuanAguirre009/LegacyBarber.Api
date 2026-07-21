namespace LegacyBarber.App.Core.Model.Identity
{
    /// <summary>
    /// Data required by a barbershop owner to create their barbershop.
    /// </summary>
    public class CrearBarberiaModel
    {
        public string Nombre { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string EmailContacto { get; set; } = string.Empty;
        public long? LogoId { get; set; }
    }
}
