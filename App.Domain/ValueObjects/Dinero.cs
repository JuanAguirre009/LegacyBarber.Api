namespace LegacyBarber.App.Domain.ValueObjects
{
    /// <summary>
    /// Represents an amount of money with its currency.
    /// </summary>
    public sealed class Dinero : IEquatable<Dinero>
    {
        public decimal Monto { get; }
        public string Moneda { get; }

        public Dinero(decimal monto, string moneda = "COP")
        {
            if (monto < 0)
                throw new ArgumentException("El monto no puede ser negativo.", nameof(monto));

            if (string.IsNullOrWhiteSpace(moneda))
                throw new ArgumentException("La moneda es obligatoria.", nameof(moneda));

            Monto = monto;
            Moneda = moneda.ToUpperInvariant();
        }

        public static Dinero operator +(Dinero a, Dinero b)
        {
            if (a.Moneda != b.Moneda)
                throw new InvalidOperationException("No se pueden sumar montos de diferentes monedas.");

            return new Dinero(a.Monto + b.Monto, a.Moneda);
        }

        public bool Equals(Dinero? other)
        {
            if (other is null)
                return false;

            return Monto == other.Monto && Moneda == other.Moneda;
        }

        public override bool Equals(object? obj) => Equals(obj as Dinero);

        public override int GetHashCode() => HashCode.Combine(Monto, Moneda);

        public override string ToString() => $"{Monto:C} {Moneda}";
    }
}
