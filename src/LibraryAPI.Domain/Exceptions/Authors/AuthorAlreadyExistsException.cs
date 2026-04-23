using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Authors;

/// <summary>
/// Se lanza cuando se intenta crear un autor con un nombre que ya existe en el sistema.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class AuthorAlreadyExistsException : DomainException
{
    public string FullName { get; }

    public AuthorAlreadyExistsException(string fullName)
        : base("AUTHOR_ALREADY_EXISTS", DomainErrors.Author.AuthorAlreadyExists)
    {
        FullName = fullName;
    }
}
