namespace LegacyBarber.App.Core.Model.Identity
{
    public class ActualizarCategoriaModel
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public short Orden { get; set; }
        public bool Activa { get; set; } = true;
    }
}
