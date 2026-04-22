using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Users;

/// <summary>
/// Se publica cuando un nuevo usuario es registrado en el sistema.
/// Los handlers pueden reaccionar enviando un email de bienvenida.
/// </summary>
public sealed class UserCreatedEvent : BaseDomainEvent
{
    /// <summary>
    /// Usuario que fue registrado.
    /// </summary>
    public User User { get; }

    public UserCreatedEvent(User user)
    {
        User = user;
    }
}