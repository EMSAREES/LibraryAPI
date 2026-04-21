using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.ValueObjects;

/// <summary>
/// Representa un código ISBN (International Standard Book Number) válido.
/// Acepta ISBN-10 e ISBN-13. Se usa en Book como identificador único editorial.
/// </summary>
public sealed class Isbn : IEquatable<Isbn>
{
    /// <summary>
    /// Valor del ISBN normalizado sin guiones ni espacios.
    /// </summary>
    public string Value { get; }

    private Isbn(string value) => Value = value;

    /// <summary>
    /// Crea una instancia de Isbn validando longitud e ISBN-13.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si el ISBN es nulo, vacío, tiene longitud incorrecta
    /// o no pasa la validación de dígito de control ISBN-13.
    /// </exception>
    public static Isbn Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainValidationException(DomainErrors.Book.IsbnRequired)
            {
                FieldName = nameof(Isbn)
            };

        // Eliminar guiones y espacios para normalizar
        var normalized = value.Replace("-", "").Replace(" ", "").Trim();

        if (normalized.Length != 10 && normalized.Length != 13)
            throw new DomainValidationException(
                "El ISBN debe tener exactamente 10 o 13 dígitos.")
            {
                FieldName = nameof(Isbn)
            };

        if (!normalized.All(char.IsDigit))
            throw new DomainValidationException(
                "El ISBN solo debe contener dígitos numéricos.")
            {
                FieldName = nameof(Isbn)
            };

        if (normalized.Length == 13 && !IsValidIsbn13(normalized))
            throw new DomainValidationException(
                "El ISBN-13 proporcionado no es válido según su dígito de control.")
            {
                FieldName = nameof(Isbn)
            };

        return new Isbn(normalized);
    }

    /// <summary>
    /// Valida el dígito de control de un ISBN-13.
    /// </summary>
    private static bool IsValidIsbn13(string isbn)
    {
        var sum = 0;

        for (var i = 0; i < 12; i++)
        {
            var digit = isbn[i] - '0';
            sum += i % 2 == 0 ? digit : digit * 3;
        }

        var checkDigit = (10 - sum % 10) % 10;
        return checkDigit == isbn[12] - '0';
    }

    /// <summary>
    /// Compara esta instancia de Isbn con otra,
    /// verificando que no sea nula y que ambas tengan el mismo valor normalizado.
    /// </summary>
    public bool Equals(Isbn? other) => other is not null && Value == other.Value;

    /// <summary>
    /// Sobrescribe Equals para permitir comparación con objetos genéricos.
    /// Si el objeto es un Isbn, delega la comparación al método Equals específico.
    /// </summary>
    public override bool Equals(object? obj) => obj is Isbn other && Equals(other);

    /// <summary>
    /// Genera un código hash basado en el valor del ISBN.
    /// Se usa en colecciones como diccionarios o conjuntos para identificar instancias únicas.
    /// </summary>
    public override int GetHashCode() => Value.GetHashCode();

    /// <summary>
    /// Devuelve el ISBN como cadena normalizada.
    /// Útil para mostrarlo directamente en interfaces o registros.
    /// </summary>
    public override string ToString() => Value;

    /// <summary>
    /// Conversión implícita de Isbn a string.
    /// Permite asignar un Isbn directamente a una variable string sin necesidad de llamar a .Value.
    /// </summary>
    public static implicit operator string(Isbn isbn) => isbn.Value;

}