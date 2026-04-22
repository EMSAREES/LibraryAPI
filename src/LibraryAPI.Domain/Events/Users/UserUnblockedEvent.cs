using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Users;

/// <summary>
/// Se publica cuando un usuario es desbloqueado después de
/// saldar todas sus multas pendientes.
/// </summary>
public sealed class UserUnblockedEvent : BaseDomainEvent
{
    /// <summary>
    /// Usuario que fue desbloqueado.
    /// </summary>
    public User User { get; }

    public UserUnblockedEvent(User user)
    {
        User = user;
    }
}