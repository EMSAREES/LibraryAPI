using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Authors;

/// <summary>
/// Se lanza cuando se busca un autor por ID y no existe en el sistema.
/// HTTP → 404 Not Found.
/// </summary>
public sealed class AuthorNotFoundException : DomainException
{
    public Guid AuthorId { get; }

    public AuthorNotFoundException(Guid authorId)
        : base("AUTHOR_NOT_FOUND", DomainErrors.Author.AuthorNotFound)
    {
        AuthorId = authorId;
    }
}
