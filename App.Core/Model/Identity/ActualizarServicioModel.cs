namespace LegacyBarber.App.Core.Model.Identity
{
    public class ActualizarServicioModel
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public short DuracionMinutos { get; set; }
        public decimal Precio { get; set; }
        public long? CategoriaId { get; set; }
        public string? Color { get; set; }
        public long? ImagenId { get; set; }
    }
}
