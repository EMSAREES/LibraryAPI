using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Users;

/// <summary>
/// Se lanza cuando se intenta operar con un usuario inactivo.
/// HTTP → 404 Not Found.
/// </summary>
public sealed class UserNotActiveException : DomainException
{
    public UserNotActiveException()
        : base("USER_NOT_ACTIVE", DomainErrors.User.NotActive) { }
}