using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Users;

/// <summary>
/// Se lanza cuando se intenta desbloquear a un usuario que no está bloqueado.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class UserNotBlockedException : DomainException
{
    public UserNotBlockedException()
        : base("USER_NOT_BLOCKED", DomainErrors.User.NotBlocked) { }
}
