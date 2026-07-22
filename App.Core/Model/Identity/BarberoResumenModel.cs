namespace LegacyBarber.App.Core.Model.Identity
{
    public class BarberoResumenModel
    {
        public long ServicioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public short DuracionMinutos { get; set; }
        public decimal Precio { get; set; }
    }
}
