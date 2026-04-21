using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.ValueObjects;

/// <summary>
/// Representa un valor monetario seguro dentro del dominio.
/// Se usa en Fine para el monto por día y el total de la multa.
/// No permite valores negativos ni precisión mayor a 2 decimales.
/// </summary>
public sealed class Money : IEquatable<Money>, IComparable<Money>
{
    /// <summary>
    /// Monto monetario con precisión de 2 decimales.
    /// </summary>
    public decimal Amount { get; }

    private Money(decimal amount) => Amount = Math.Round(amount, 2);

    /// <summary>
    /// Crea una instancia de Money validando que el monto no sea negativo.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si el monto es negativo.
    /// </exception>
    public static Money Create(decimal amount)
    {
        if (amount < 0)
            throw new DomainValidationException(DomainErrors.Validation.NegativeValue)
            {
                FieldName = nameof(Money)
            };

        return new Money(amount);
    }

    /// <summary>
    /// Instancia que representa cero pesos/unidades monetarias.
    /// </summary>
    public static Money Zero => new(0);

    /// <summary>
    /// Suma dos montos y retorna un nuevo Money con el resultado.
    /// </summary>
    public Money Add(Money other) => new(Amount + other.Amount);

    /// <summary>
    /// Resta otro monto y retorna un nuevo Money con el resultado.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si el resultado es negativo.
    /// </exception>
    public Money Subtract(Money other)
    {
        var result = Amount - other.Amount;

        if (result < 0)
            throw new DomainValidationException(DomainErrors.Validation.NegativeValue);

        return new Money(result);
    }

    /// <summary>
    /// Multiplica el monto por un número entero (ej. días de retraso).
    /// </summary>
    public Money MultiplyBy(int factor) => new(Amount * factor);

    /// <summary>
    /// Indica si el monto es mayor a cero.
    /// </summary>
    public bool IsPositive => Amount > 0;

    /// <summary>
    /// Indica si el monto es exactamente cero.
    /// </summary>
    public bool IsZero => Amount == 0;

    /// <summary>
    /// Compara esta instancia de Money con otra.
    /// Retorna 1 si la otra es nula, o el resultado de comparar ambos montos.
    /// </summary>
    public int CompareTo(Money? other) => other is null ? 1 : Amount.CompareTo(other.Amount);

    /// <summary>
    /// Verifica igualdad estructural entre dos instancias de Money.
    /// Retorna true si ambas tienen el mismo monto.
    /// </summary>
    public bool Equals(Money? other) => other is not null && Amount == other.Amount;

    /// <summary>
    /// Sobrescribe Equals para permitir comparación con objetos genéricos.
    /// Si el objeto es Money, delega la comparación al método Equals específico.
    /// </summary>
    public override bool Equals(object? obj) => obj is Money other && Equals(other);

    /// <summary>
    /// Genera un código hash basado en el monto.
    /// Se usa en colecciones como diccionarios o conjuntos para identificar instancias únicas.
    /// </summary>
    public override int GetHashCode() => Amount.GetHashCode();

    /// <summary>
    /// Devuelve el monto como cadena con dos decimales.
    /// Útil para mostrar valores monetarios en interfaces o registros.
    /// </summary>
    public override string ToString() => Amount.ToString("F2");

    /// <summary>
    /// Operador mayor que: compara dos instancias de Money.
    /// </summary>
    public static bool operator >(Money left, Money right) => left.Amount > right.Amount;

    /// <summary>
    /// Operador menor que: compara dos instancias de Money.
    /// </summary>
    public static bool operator <(Money left, Money right) => left.Amount < right.Amount;

    /// <summary>
    /// Operador mayor o igual que: compara dos instancias de Money.
    /// </summary>
    public static bool operator >=(Money left, Money right) => left.Amount >= right.Amount;

    /// <summary>
    /// Operador menor o igual que: compara dos instancias de Money.
    /// </summary>
    public static bool operator <=(Money left, Money right) => left.Amount <= right.Amount;

}