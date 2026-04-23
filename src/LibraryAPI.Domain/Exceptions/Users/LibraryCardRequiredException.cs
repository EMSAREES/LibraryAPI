using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Users;

/// <summary>
/// Se lanza cuando se requiere una tarjeta de biblioteca para una operación y el usuario no la tiene.
/// HTTP → 400 Bad Request.
/// </summary>
public sealed class LibraryCardRequiredException : DomainException
{
    public LibraryCardRequiredException()
        : base("LIBRARY_CARD_REQUIRED", DomainErrors.User.LibraryCardRequired) { }
}
