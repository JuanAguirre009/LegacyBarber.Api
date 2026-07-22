namespace LegacyBarber.App.Core.Model.Identity
{
    public class CategoriaModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public short Orden { get; set; }
        public bool Activa { get; set; }
        public int CantidadServicios { get; set; }
    }
}
