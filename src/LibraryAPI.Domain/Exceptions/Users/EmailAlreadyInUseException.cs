using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Users;

/// <summary>
/// Se lanza cuando se intenta registrar un usuario con un correo
/// que ya está asociado a otra cuenta activa en el sistema.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class EmailAlreadyInUseException : DomainException
{
    public string Email { get; }

    public EmailAlreadyInUseException(string email)
        : base("EMAIL_ALREADY_IN_USE", DomainErrors.User.EmailAlreadyInUse)
    {
        Email = email;
    }
}