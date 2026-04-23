using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Fines;

/// <summary>
/// Se publica cuando una multa es pagada por el usuario.
/// Los handlers deben verificar si el usuario ya no tiene multas pendientes
/// para desbloquearlo automáticamente.
/// </summary>
public sealed class FinePaidEvent : BaseDomainEvent
{
    /// <summary>
    /// Multa que fue pagada.
    /// </summary>
    public Fine Fine { get; }

    /// <summary>
    /// Identificador del usuario o empleado que registró el pago.
    /// </summary>
    public Guid PaidByUserId { get; }

    public FinePaidEvent(Fine fine, Guid paidByUserId)
    {
        Fine = fine;
        PaidByUserId = paidByUserId;
    }
}