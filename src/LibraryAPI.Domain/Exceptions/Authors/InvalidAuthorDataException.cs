using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Authors;

/// <summary>
/// Se lanza cuando los datos del autor no cumplen con las reglas de negocio.
/// Por ejemplo: fecha de nacimiento en el futuro, nombre inválido, etc.
/// HTTP → 400 Bad Request.
/// </summary>
public sealed class InvalidAuthorDataException : DomainException
{
    public string? FieldName { get; set; }

    public InvalidAuthorDataException(string message)
        : base("INVALID_AUTHOR_DATA", message)
    {
    }
}
