namespace LegacyBarber.App.Domain.Entities
{
    public sealed class EstadoCita
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Color { get; set; }

        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}
