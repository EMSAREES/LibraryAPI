using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.ValueObjects;

/// <summary>
/// Representa una imagen asociada a un libro (portada o contraportada).
/// Encapsula la URL y el texto alternativo como una unidad con validaciones propias.
/// </summary>
public sealed class Imagen : IEquatable<Imagen>
{
    public string Url { get; }
    public string AltText { get; }

    // Constructor privado para EF Core
    private Imagen() 
    { 
        Url = string.Empty;
        AltText = string.Empty;
    }

    private Imagen(string url, string altText)
    {
        Url = url;
        AltText = altText;
    }

    /// <summary>
    /// Crea una instancia de Imagen validando que la URL y el texto
    /// alternativo no estén vacíos ni superen la longitud máxima.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si la URL o el texto alternativo son nulos o vacíos.
    /// </exception>
    public static Imagen Create(string url, string altText)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(Url)
            };

        if (string.IsNullOrWhiteSpace(altText))
            throw new DomainValidationException(DomainErrors.Validation.ValueRequired)
            {
                FieldName = nameof(AltText)
            };

        if (url.Trim().Length > 2000)
            throw new DomainValidationException(DomainErrors.Validation.StringTooLong)
            {
                FieldName = nameof(Url)
            };

        if (altText.Trim().Length > 300)
            throw new DomainValidationException(DomainErrors.Validation.StringTooLong)
            {
                FieldName = nameof(AltText)
            };

        return new Imagen(url.Trim(), altText.Trim());
    }

    public bool Equals(Imagen? other) =>
        other is not null &&
        Url == other.Url &&
        AltText == other.AltText;

    public override bool Equals(object? obj) => obj is Imagen other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(Url, AltText);

    public override string ToString() => $"{Url} ({AltText})";
}