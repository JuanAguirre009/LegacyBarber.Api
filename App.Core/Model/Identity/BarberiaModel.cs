namespace LegacyBarber.App.Core.Model.Identity
{
    /// <summary>
    /// Full representation of a barbershop for its owner or admin.
    /// Contains internal fields such as state and activation date
    /// that are not exposed in the public marketplace summary.
    /// </summary>
    public class BarberiaModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
        public string? Telefono { get; set; }
        public string Email { get; set; } = string.Empty;
        public string HorarioAtencion { get; set; } = "{}";
        public long? LogoId { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime? FechaActivacion { get; set; }
        public DateTime CreadoEn { get; set; }
        public DateTime? ActualizadoEn { get; set; }
    }
}
