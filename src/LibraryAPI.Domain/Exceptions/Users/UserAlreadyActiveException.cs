using LibraryAPI.Domain.Exceptions.Base;

namespace LibraryAPI.Domain.Exceptions.Users;

/// <summary>
/// Se lanza cuando se intenta activar un usuario que ya se encuentra activo.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class UserAlreadyActiveException : DomainException
{
    public UserAlreadyActiveException()
        : base("USER_ALREADY_ACTIVE", "El usuario ya se encuentra activo en el sistema.") { }
}