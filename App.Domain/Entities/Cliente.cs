namespace LegacyBarber.App.Domain.Entities
{
    public sealed class Cliente
    {
        public long Id { get; set; }
        public long BarberiaId { get; set; }
        public long UsuarioId { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Notas { get; set; }
        public string Preferencias { get; set; } = "{}";
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        public Barberia Barberia { get; set; } = default!;
        public Usuario Usuario { get; set; } = default!;
        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
        public ICollection<Resena> Resenas { get; set; } = new List<Resena>();
    }
}
