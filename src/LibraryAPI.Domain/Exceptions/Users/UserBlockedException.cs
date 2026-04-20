using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Users;

/// <summary>
/// Se lanza cuando un usuario bloqueado intenta realizar una operación
/// que requiere cuenta activa, como crear un préstamo o una reserva.
/// HTTP → 403 Forbidden.
/// </summary>
public sealed class UserBlockedException : DomainException
{
    public UserBlockedException()
        : base("USER_BLOCKED", DomainErrors.User.AlreadyBlocked) { }
}