using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.ValueObjects;

public sealed class Imagen : IEquatable<Imagen>
{
    public string Url { get; private set; } = default!;
    public string AltText { get; private set; } = default!;

    public Imagen(string url, string altText)
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

        Url = url;
        AltText = altText;
    }

    public bool Equals(Imagen? other) =>
        other is not null &&
        Url == other.Url &&
        AltText == other.AltText;

    public override bool Equals(object? obj) => obj is Imagen other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(Url, AltText);

    public override string ToString() => $"{Url} ({AltText})";
}