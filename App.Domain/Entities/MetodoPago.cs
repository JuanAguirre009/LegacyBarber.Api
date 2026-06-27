namespace LegacyBarber.App.Domain.Entities
{
    public sealed class MetodoPago
    {
        public long Id { get; set; }
        public long BarberiaId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public string Configuracion { get; set; } = "{}";

        public Barberia Barberia { get; set; } = default!;
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}
