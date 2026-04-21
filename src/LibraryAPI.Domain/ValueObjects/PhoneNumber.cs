using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.ValueObjects;

/// <summary>
/// Representa un número telefónico válido.
/// Se usa en User y Branch para evitar strings sin validar.
/// Solo permite dígitos, espacios, guiones y el símbolo +.
/// </summary>
public sealed class PhoneNumber : IEquatable<PhoneNumber>
{
    /// <summary>
    /// Número de teléfono normalizado sin espacios extra.
    /// </summary>
    public string Value { get; }

    private PhoneNumber(string value) => Value = value;

    /// <summary>
    /// Crea una instancia de PhoneNumber validando formato y longitud.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si el número es nulo, vacío, tiene caracteres inválidos
    /// o supera la longitud máxima de 20 caracteres.
    /// </exception>
    public static PhoneNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(PhoneNumber)
            };

        var normalized = value.Trim();

        if (normalized.Length > 20)
            throw new DomainValidationException(DomainErrors.Validation.StringTooLong)
            {
                FieldName = nameof(PhoneNumber)
            };

        foreach (var ch in normalized)
        {
            if (!char.IsDigit(ch) && ch != '+' && ch != '-' && ch != ' ' && ch != '(' && ch != ')')
                throw new DomainValidationException(
                    "El número de teléfono contiene caracteres no permitidos.")
                {
                    FieldName = nameof(PhoneNumber)
                };
        }

        return new PhoneNumber(normalized);
    }

    /// <summary>
    /// Compara esta instancia de PhoneNumber con otra,
    /// verificando que no sea nula y que ambas tengan el mismo valor normalizado.
    /// </summary>
    public bool Equals(PhoneNumber? other) => other is not null && Value == other.Value;

    /// <summary>
    /// Sobrescribe Equals para permitir comparación con objetos genéricos.
    /// Si el objeto es un PhoneNumber, delega la comparación al método Equals específico.
    /// </summary>
    public override bool Equals(object? obj) => obj is PhoneNumber other && Equals(other);

    /// <summary>
    /// Genera un código hash basado en el valor del número.
    /// Se usa en colecciones como diccionarios o conjuntos para identificar instancias únicas.
    /// </summary>
    public override int GetHashCode() => Value.GetHashCode();

    /// <summary>
    /// Devuelve el número de teléfono como cadena.
    /// Útil para mostrarlo directamente en interfaces o registros.
    /// </summary>
    public override string ToString() => Value;

    /// <summary>
    /// Conversión implícita de PhoneNumber a string.
    /// Permite asignar un PhoneNumber directamente a una variable string sin necesidad de llamar a .Value.
    /// </summary>
    public static implicit operator string(PhoneNumber phone) => phone.Value;

}