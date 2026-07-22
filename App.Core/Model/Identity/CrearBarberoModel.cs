namespace LegacyBarber.App.Core.Model.Identity
{
    public class CrearBarberoModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Biografia { get; set; }
        public string? Especialidades { get; set; }
        public short? AniosExperiencia { get; set; }
        public decimal ComisionPorcentaje { get; set; } = 0;
        public ICollection<long> ServicioIds { get; set; } = new List<long>();
    }
}
