namespace LegacyBarber.App.Core.Model.Identity
{
    public class ServicioModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public short DuracionMinutos { get; set; }
        public decimal Precio { get; set; }
        public string? Color { get; set; }
        public bool Activo { get; set; }
        public long? CategoriaId { get; set; }
        public string? CategoriaNombre { get; set; }
        public long? ImagenId { get; set; }
        public DateTime CreadoEn { get; set; }
    }
}
