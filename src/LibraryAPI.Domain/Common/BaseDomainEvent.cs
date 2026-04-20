namespace LibraryAPI.Domain.Common;

/// <summary>
/// Implementación base para todos los eventos de dominio.
/// Provee identificador único y marca de tiempo automática.
/// </summary>
public abstract class BaseDomainEvent : IDomainEvent
{
    /// <summary>
    /// Identificador único del evento generado en el momento de su creación.
    /// </summary>
    public Guid EventId { get; } = Guid.NewGuid();

    /// <summary>
    /// Fecha y hora UTC en que ocurrió el evento.
    /// </summary>
    public DateTime OccurredAt { get; } = DateTime.UtcNow;
}