using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Users;

/// <summary>
/// Se lanza cuando se busca un usuario por ID o email y no existe en el sistema.
/// HTTP → 404 Not Found.
/// </summary>
public sealed class UserNotFoundException : DomainException
{
    public Guid UserId { get; }

    public UserNotFoundException(Guid userId)
        : base("USER_NOT_FOUND", DomainErrors.User.NotFound)
    {
        UserId = userId;
    }
}