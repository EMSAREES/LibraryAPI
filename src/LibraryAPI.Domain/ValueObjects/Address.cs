using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.ValueObjects;

/// <summary>
/// Representa la dirección física de una sucursal de biblioteca.
/// Encapsula calle, ciudad y teléfono como una unidad coherente.
/// Se usa en Branch para evitar propiedades dispersas sin validar.
/// </summary>
public sealed class Address : IEquatable<Address>
{
    /// <summary>
    /// Calle, número y colonia de la sucursal.
    /// </summary>
    public string Street { get; }

    /// <summary>
    /// Ciudad donde se ubica la sucursal.
    /// </summary>
    public string City { get; }

    /// <summary>
    /// Representación completa de la dirección formateada.
    /// </summary>
    public string DisplayAddress => $"{Street}, {City}";

    private Address(string street, string city)
    {
        Street = street;
        City = city;
    }

    /// <summary>
    /// Crea una instancia de Address validando que los campos
    /// obligatorios no estén vacíos y no superen la longitud máxima.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si algún campo requerido es nulo, vacío o demasiado largo.
    /// </exception>
    public static Address Create(string street, string city)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(Street)
            };

        if (string.IsNullOrWhiteSpace(city))
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(City)
            };

        if (street.Trim().Length > 300)
            throw new DomainValidationException(DomainErrors.Validation.StringTooLong)
            {
                FieldName = nameof(Street)
            };

        if (city.Trim().Length > 100)
            throw new DomainValidationException(DomainErrors.Validation.StringTooLong)
            {
                FieldName = nameof(City)
            };

        return new Address(street.Trim(), city.Trim());
    }

    /// <summary>
    /// Compara esta instancia de Address con otra,
    /// verificando que no sea nula y que tanto la calle como la ciudad coincidan.
    /// </summary>
    public bool Equals(Address? other) =>
        other is not null &&
        Street == other.Street &&
        City == other.City;

    /// <summary>
    /// Sobrescribe Equals para permitir comparación con objetos genéricos.
    /// Si el objeto es un Address, delega la comparación al método Equals específico.
    /// </summary>
    public override bool Equals(object? obj) => obj is Address other && Equals(other);

    /// <summary>
    /// Genera un código hash combinando calle y ciudad.
    /// Se usa en colecciones como diccionarios o conjuntos para identificar instancias únicas.
    /// </summary>
    public override int GetHashCode() => HashCode.Combine(Street, City);

    /// <summary>
    /// Devuelve la dirección formateada como cadena (calle + ciudad).
    /// Útil para mostrar la dirección en interfaces o registros.
    /// </summary>
    public override string ToString() => DisplayAddress;

}