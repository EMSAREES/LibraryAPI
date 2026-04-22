using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Users;

/// <summary>
/// Se publica cuando un usuario es bloqueado por multas pendientes
/// o por acción administrativa.
/// Los handlers pueden reaccionar enviando una notificación al usuario.
/// </summary>
public sealed class UserBlockedEvent : BaseDomainEvent
{
    /// <summary>
    /// Usuario que fue bloqueado.
    /// </summary>
    public User User { get; }

    public UserBlockedEvent(User user)
    {
        User = user;
    }
}