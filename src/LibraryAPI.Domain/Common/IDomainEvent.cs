using MediatR;

namespace LibraryAPI.Domain.Common;

/// <summary>
/// Contrato base para todos los eventos de dominio del sistema.
/// Implementa INotification para integrarse con MediatR.
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    /// Identificador único del evento.
    /// </summary>
    Guid EventId { get; }

    /// <summary>
    /// Fecha y hora exacta en que ocurrió el evento.
    /// </summary>
    DateTime OccurredAt { get; }
}