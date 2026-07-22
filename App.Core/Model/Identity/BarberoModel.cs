namespace LegacyBarber.App.Core.Model.Identity
{
    public class BarberoModel
    {
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Biografia { get; set; }
        public string Especialidades { get; set; } = "[]";
        public short? AniosExperiencia { get; set; }
        public decimal ComisionPorcentaje { get; set; }
        public decimal CalificacionPromedio { get; set; }
        public int TotalResenas { get; set; }
        public bool Activo { get; set; }
        public ICollection<BarberoResumenModel> Servicios { get; set; } = new List<BarberoResumenModel>();
    }
}
