namespace LegacyBarber.App.Core.Model
{
    public class AuditableModel
    {
        public string? Usuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
