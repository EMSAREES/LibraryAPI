using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Users;

/// <summary>
/// Se lanza cuando se intenta bloquear un usuario que ya está bloqueado.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class UserAlreadyBlockedException : DomainException
{
    public UserAlreadyBlockedException()
        : base("USER_ALREADY_BLOCKED", DomainErrors.User.AlreadyBlocked) { }
}