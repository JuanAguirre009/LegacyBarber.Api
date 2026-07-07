namespace LegacyBarber.App.Domain.ValueObjects
{
    /// <summary>
    /// Represents the weekly opening schedule of a barbershop.
    /// </summary>
    public sealed class HorarioAtencion
    {
        private readonly List<FranjaHoraria> franjas = new();

        public IReadOnlyCollection<FranjaHoraria> Franjas => franjas.AsReadOnly();

        public HorarioAtencion(IEnumerable<FranjaHoraria> franjas)
        {
            this.franjas = franjas?.ToList() ?? new List<FranjaHoraria>();
        }

        public static HorarioAtencion Vacio() => new(Array.Empty<FranjaHoraria>());

        public bool EstaAbierto(DayOfWeek dia, TimeSpan hora)
        {
            return franjas.Any(f => f.Dia == dia && f.HoraInicio <= hora && f.HoraFin > hora);
        }
    }

    /// <summary>
    /// A single time slot for a given day of the week.
    /// </summary>
    public sealed class FranjaHoraria
    {
        public DayOfWeek Dia { get; }
        public TimeSpan HoraInicio { get; }
        public TimeSpan HoraFin { get; }

        public FranjaHoraria(DayOfWeek dia, TimeSpan horaInicio, TimeSpan horaFin)
        {
            if (horaInicio >= horaFin)
                throw new ArgumentException("La hora de inicio debe ser menor que la hora de fin.");

            Dia = dia;
            HoraInicio = horaInicio;
            HoraFin = horaFin;
        }
    }
}
