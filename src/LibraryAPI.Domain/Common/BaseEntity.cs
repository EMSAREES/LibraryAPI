namespace LibraryAPI.Domain.Common;

/// <summary>
/// Clase base para todas las entidades del dominio.
/// Proporciona identidad única, control de eventos de dominio
/// y marcas de tiempo de auditoría.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Lista interna de eventos de dominio generados por la entidad.
    /// Se utiliza para acumular notificaciones de cambios significativos
    /// en el modelo de negocio (ej. préstamo de libro, devolución, registro de usuario).
    /// Los eventos se publican posteriormente desde la capa de aplicación o infraestructura.
    /// </summary>
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    /// Identificador único de la entidad generado automáticamente.
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Fecha y hora en que fue creado el registro.
    /// </summary>
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// Fecha y hora de la última actualización del registro.
    /// </summary>
    public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;

    /// <summary>
    /// Identificador del usuario que creó el registro.
    /// </summary>
    public Guid CreatedByUserId { get; init; }

    /// <summary>
    /// Lista de eventos de dominio pendientes de publicar.
    /// </summary>
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    /// <summary>
    /// Agrega un evento de dominio a la cola de eventos pendientes.
    /// </summary>
    protected void AddDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);

    /// <summary>
    /// Limpia todos los eventos de dominio después de haber sido publicados.
    /// </summary>
    public void ClearDomainEvents() => _domainEvents.Clear();

    /// <summary>
    /// Actualiza la marca de tiempo de modificación al momento actual.
    /// </summary>
    protected void MarkAsUpdated() => UpdatedAt = DateTime.UtcNow;
}
