using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.ValueObjects;

/// <summary>
/// Representa el nombre completo de una persona (usuario o autor).
/// Encapsula FirstName y LastName como una unidad con sus propias validaciones.
/// </summary>
public sealed class FullName : IEquatable<FullName>
{
    /// <summary>
    /// Nombre(s) de la persona.
    /// </summary>
    public string FirstName { get; }

    /// <summary>
    /// Apellido(s) de la persona.
    /// </summary>
    public string LastName { get; }

    /// <summary>
    /// Nombre completo formateado: "Nombre Apellido".
    /// </summary>
    public string DisplayName => $"{FirstName} {LastName}";

    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    /// <summary>
    /// Crea una instancia de FullName validando que ambos campos no estén vacíos
    /// y no superen la longitud máxima permitida.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si alguno de los campos es nulo, vacío o demasiado largo.
    /// </exception>
    public static FullName Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(FirstName)
            };

        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(LastName)
            };

        if (firstName.Trim().Length > 100)
            throw new DomainValidationException(DomainErrors.Validation.StringTooLong)
            {
                FieldName = nameof(FirstName)
            };

        if (lastName.Trim().Length > 100)
            throw new DomainValidationException(DomainErrors.Validation.StringTooLong)
            {
                FieldName = nameof(LastName)
            };

        return new FullName(
            firstName.Trim(),
            lastName.Trim()
        );
    }

    /// <summary>
    /// Compara esta instancia de FullName con otra,
    /// verificando que no sea nula y que tanto el nombre como el apellido coincidan.
    /// </summary>
    public bool Equals(FullName? other) =>
        other is not null &&
        FirstName == other.FirstName &&
        LastName == other.LastName;

    /// <summary>
    /// Sobrescribe Equals para permitir comparación con objetos genéricos.
    /// Si el objeto es un FullName, delega la comparación al método Equals específico.
    /// </summary>
    public override bool Equals(object? obj) => obj is FullName other && Equals(other);

    /// <summary>
    /// Genera un código hash combinando nombre y apellido.
    /// Se usa en colecciones como diccionarios o conjuntos para identificar instancias únicas.
    /// </summary>
    public override int GetHashCode() => HashCode.Combine(FirstName, LastName);

    /// <summary>
    /// Devuelve la representación textual del nombre completo.
    /// Útil para mostrar el nombre en interfaces o registros.
    /// </summary>
    public override string ToString() => DisplayName;

}