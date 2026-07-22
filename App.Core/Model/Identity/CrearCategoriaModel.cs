namespace LegacyBarber.App.Core.Model.Identity
{
    public class CrearCategoriaModel
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public short Orden { get; set; } = 0;
    }
}
