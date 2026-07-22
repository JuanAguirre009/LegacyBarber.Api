namespace LegacyBarber.App.Core.Model.Identity
{
    public class ActualizarBarberoModel
    {
        public string NombreCompleto { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Biografia { get; set; }
        public string? Especialidades { get; set; }
        public short? AniosExperiencia { get; set; }
        public decimal ComisionPorcentaje { get; set; }
        public bool Activo { get; set; } = true;
    }
}
