using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.ValueObjects;

/// <summary>
/// Representa las imágenes de portada y contraportada de un libro.
/// Encapsula ambas URLs como una unidad validada.
/// Las imágenes se almacenan en un servicio externo (Cloudinary/S3)
/// y aquí solo se guarda la URL resultante.
/// </summary>
public sealed class BookCover : IEquatable<BookCover>
{
    /// <summary>
    /// URL de la imagen de portada del libro.
    /// </summary>
    public string? CoverImageUrl { get; }

    /// <summary>
    /// URL de la imagen de contraportada del libro.
    /// </summary>
    public string? BackCoverImageUrl { get; }

    /// <summary>
    /// Indica si el libro tiene al menos una imagen de portada registrada.
    /// </summary>
    public bool HasCover => !string.IsNullOrWhiteSpace(CoverImageUrl);

    /// <summary>
    /// Indica si el libro tiene imagen de contraportada registrada.
    /// </summary>
    public bool HasBackCover => !string.IsNullOrWhiteSpace(BackCoverImageUrl);

    private BookCover(string? coverImageUrl, string? backCoverImageUrl)
    {
        CoverImageUrl = coverImageUrl;
        BackCoverImageUrl = backCoverImageUrl;
    }

    /// <summary>
    /// Crea una instancia de BookCover validando que las URLs no superen
    /// la longitud máxima de 500 caracteres.
    /// Ambos parámetros son opcionales — un libro puede no tener imágenes.
    /// </summary>
    /// <exception cref="DomainValidationException">
    /// Se lanza si alguna URL supera los 500 caracteres o tiene formato inválido.
    /// </exception>
    public static BookCover Create(string? coverImageUrl, string? backCoverImageUrl)
    {
        if (coverImageUrl is not null)
        {
            if (coverImageUrl.Length > 500)
                throw new DomainValidationException(DomainErrors.Validation.StringTooLong)
                {
                    FieldName = nameof(CoverImageUrl)
                };

            if (!Uri.TryCreate(coverImageUrl, UriKind.Absolute, out _))
                throw new DomainValidationException(
                    "La URL de la portada no tiene un formato válido.")
                {
                    FieldName = nameof(CoverImageUrl)
                };
        }

        if (backCoverImageUrl is not null)
        {
            if (backCoverImageUrl.Length > 500)
                throw new DomainValidationException(DomainErrors.Validation.StringTooLong)
                {
                    FieldName = nameof(BackCoverImageUrl)
                };

            if (!Uri.TryCreate(backCoverImageUrl, UriKind.Absolute, out _))
                throw new DomainValidationException(
                    "La URL de la contraportada no tiene un formato válido.")
                {
                    FieldName = nameof(BackCoverImageUrl)
                };
        }

        return new BookCover(coverImageUrl, backCoverImageUrl);
    }

    /// <summary>
    /// Instancia vacía para libros sin imágenes registradas.
    /// </summary>
    public static BookCover Empty => new(null, null);

    /// <summary>
    /// Retorna una nueva instancia con la portada actualizada.
    /// </summary>
    public BookCover WithCover(string coverImageUrl) =>
        Create(coverImageUrl, BackCoverImageUrl);

    /// <summary>
    /// Retorna una nueva instancia con la contraportada actualizada.
    /// </summary>
    public BookCover WithBackCover(string backCoverImageUrl) =>
        Create(CoverImageUrl, backCoverImageUrl);

    /// <summary>
    /// Compara esta instancia de BookCover con otra,
    /// verificando que no sea nula y que ambas URLs coincidan.
    /// </summary>
    public bool Equals(BookCover? other) =>
        other is not null &&
        CoverImageUrl == other.CoverImageUrl &&
        BackCoverImageUrl == other.BackCoverImageUrl;

    /// <summary>
    /// Sobrescribe Equals para permitir comparación con objetos genéricos.
    /// Si el objeto es un BookCover, delega la comparación al método Equals específico.
    /// </summary>
    public override bool Equals(object? obj) => obj is BookCover other && Equals(other);

    /// <summary>
    /// Genera un código hash combinando las URLs de portada y contraportada.
    /// Se usa en colecciones como diccionarios o conjuntos para identificar instancias únicas.
    /// </summary>
    public override int GetHashCode() =>
        HashCode.Combine(CoverImageUrl, BackCoverImageUrl);

}