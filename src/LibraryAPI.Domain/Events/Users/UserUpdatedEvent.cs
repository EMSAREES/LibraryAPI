using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Users;

/// <summary>
/// Se publica cuando un usuario actualiza su perfil.
/// </summary>
public sealed class UserUpdatedEvent : BaseDomainEvent
{
    /// <summary>
    /// Usuario con los datos actualizados.
    /// </summary>
    public User User { get; }

    public UserUpdatedEvent(User user)
    {
        User = user;
    }
}