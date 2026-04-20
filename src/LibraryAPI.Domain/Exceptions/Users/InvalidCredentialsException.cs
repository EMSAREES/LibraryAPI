using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Users;

/// <summary>
/// Se lanza cuando el email o contraseña proporcionados en el login son incorrectos.
/// Se usa un mensaje genérico por seguridad para no revelar cuál campo falló.
/// HTTP → 401 Unauthorized.
/// </summary>
public sealed class InvalidCredentialsException : DomainException
{
    public InvalidCredentialsException()
        : base("INVALID_CREDENTIALS", DomainErrors.User.InvalidCredentials) { }
}