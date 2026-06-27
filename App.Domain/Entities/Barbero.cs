namespace LegacyBarber.App.Domain.Entities
{
    public sealed class Barbero
    {
        public long Id { get; set; }
        public long BarberiaId { get; set; }
        public long UsuarioId { get; set; }
        public string? Biografia { get; set; }
        public string Especialidades { get; set; } = "[]";
        public short? AniosExperiencia { get; set; }
        public decimal ComisionPorcentaje { get; set; } = 0;
        public decimal CalificacionPromedio { get; set; } = 0;
        public int TotalResenas { get; set; } = 0;
        public bool Activo { get; set; } = true;
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        public Barberia Barberia { get; set; } = default!;
        public Usuario Usuario { get; set; } = default!;
        public ICollection<BarberoServicio> BarberoServicios { get; set; } = new List<BarberoServicio>();
        public ICollection<HorarioBarbero> Horarios { get; set; } = new List<HorarioBarbero>();
        public ICollection<ExcepcionHorario> ExcepcionesHorarios { get; set; } = new List<ExcepcionHorario>();
        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
        public ICollection<Resena> Resenas { get; set; } = new List<Resena>();
    }
}
