namespace LegacyBarber.App.Core.Model.Identity
{
    /// <summary>
    /// Public summary of a barbershop for the marketplace.
    /// </summary>
    public class BarberiaResumenModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
        public string? Telefono { get; set; }
        public string? HorarioAtencion { get; set; }
        public long? LogoId { get; set; }
    }
}
